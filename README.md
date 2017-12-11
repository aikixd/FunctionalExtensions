# FunctionalExtensions

This library provides some useful functional features for C#.

## Discrimitaned Unions

One of the most missed features in C#, Unions allow you to combine types together to beautifully express your intentions, simplify error handling, enforce compile time domain validity and much more.

### Introduction: Why should I use it?

_This section is intended to those unfamiliar with unions. Skip to the reference if you are already acquainted with them._

Suppose you have a `Person` type and a requirement that each person must have a primary contact info: `Email` or `Phone`. 

A straight forward approach would be to create two fields for both `Email` and `Phone`, add validation to the `Person` class and a switch with a flag marking currently active contact info. This approach is obviously prone to errors and resistant to changes. 

A more idiomatic approach would be to inherit the values from a common class or interface and try to compose them. But this way comes short when you need to use the values, since they are used by different services with different APIs. You end up with a choice, whether to check the type of the `ContactInfo` object or to introduce interface methods. Both approaches have their problems: the latter is introducing service dependency and polluting the type with domain methods, the former is lacks in safety and both are a pain to work with.

Unions come to solve such issues. They allow you to define which cases are available in each union and to choose a course of action in a safe way when you need to use the value. The above example can be rewritten like this:

```CSharp
class Person
{
    ContactInfo ContactInfo { get; set; }

    public void SendMessage(string msg)
    {
        ContactInfo.Match(
            email => emailService.Send(email.Value, msg),
            phone => phoneService.EnqueueCall(phone.Value, msg));
    }
}
```

This approach has several advantages above other:

  - It enforces the existance of contact info regardless of its type at compile time. As long as you don't explicitly pass null, contact info will always be there in correct state.
  - The value isn't hidden, and can be easily accessed and used, regardless of differences in APIs of each case in the union.
  - It's safe. The program will fail to compile if you forgot to handle a case.
  - Easy to change. If you add a new contact info type to the union, the program will fail to compile, forcing you to fix all places where the value is used.

Unions also help to manage null values and the infamous `NullReferenceException`. The built in `Option<T>` union exposes to you all places where a lack of value can happen:

```CSharp
db.FindPerson("Some name").Switch(
    some => Console.WriteLine(some.Value.Id),
    none => Console.WriteLine("Error: the user was not found.")); 
```

This forces you to explicitly handle the lack of value and save yourself from rewinding the program in your head to find that one place and condition which gave the null to slip in.

As a bonus, any change to the union definition will stop the project from compiling until you fix all the places where the union is consumed, thus making the code changes safe and easy. 

You would be surprised how many use cases fall under the umbrella of discriminated union solutions, and once you get familiar with them, you will start using them in places, you have never considered as problems of type composition.

### Using Unions

#### Definition

_Note: C# imposes some frustrating limitations on the elegance on how the unions could be defined. I tried to make the definition syntax as condensed as possible._

Each union is composed of number if cases. Each case can optionaly store a value. This is how an `Email` and `Phone` types composition looks like:

```CSharp
public class ContactInfo : Union<ContactInfo.Email, ContactInfo.Phone>
{
    public ContactInfo(Email @case) : base(@case) { }
    public ContactInfo(Phone @case) : base(@case) { }

    public class Email : Case<ContactInfo, Email>
    {
        public Email(Email value) : base(value) { }
    }

    public class Phone : Case<ContactInfo, Phone>
    {
        public Phone(Phone value) : base(value) { }
    }
}
```

A union is a class derived from one of the `Union<>` classes. The generic arguments are all the possible cases. The class definition has to include a constructor for each of the cases. Unfortunately C# doesn't allow such a restriction. On the bright side the program will fail the second the defined union will be used, with message explaining what constructor is missing. Also, Visual Studio can help you with auto generating the constructors. A Roslyn analizer may be added later, to attend the issue.

The cases are defined by deriving from the `Case<TUnion>` or the `Case<TUnion, TValue>` class. The latter case is used to store values with it and demands that a proper constructor is defined. Again, Visual Studio can auto generate it. 

The cases are not technically obliged to resident inside the union, but make things much more elegant, especially when the unions are generic:

```CSharp
public class Option<T> : Union<Option<T>.Some, Option<T>.None>
{
    public Option(Some @case) : base(@case) { }
    public Option(None @case) : base(@case) { }

    public class Some : Case<Option<T>, T>
    {
        public Some(T value) : base(value) { }
    }

    public class None : Case<Option<T>> { }
}
```

Here the union type argument is propagated to the nested cases. An important notion here is that the `None` case is different for each `T`, so you will not accidentially mix a `None` of one type with `None` of some other type.

__Note:__ Beware when defining cases that are called the same name as their contained types, since the compiler will try to use the case as its own contained item and you will get compilation error when trying to instanciate the case. Just use full class name instead, in the definition.

#### Creating

Creating a union is as simple as creating one of its cases:

```CSharp
var contactInfo = new ContactInfo.Email("address@example.org");

var option = new Option<int>.Some(42);
```

Each case can be implicitly converted to its containing union, so it can be safely returned or assigned as a union:

```CSharp
public ContactInfo From(Email email) =>
    new ContactInfo.Email(email);

public Option<object> Exists(object o) => 
    o == null ? 
    new Option<object>.None() : 
    new Option<object>.Some(o);
```
The union can, of cource, be created by directly invoking its constructors.

#### Consuming

Unions allow you to safely consume them and oblige you to treat all the available cases:

```CSharp
contactInfo.Match(
    email => { /* Some code */ },
    phone => { /* Some code */ });
```

The `Match` method receives a list of `Func<TCase, TValue>`'s or `Action<TCase>`'s, one for each case. The cases that are passed into the lambdas come in order they are defined in the union, are strongly typed and give the access to stored value when applicable.

When using the `Func<>` version of the method, all lambdas must return objects of the same type.

There's also a `When<TCase>` method, that can be used to access specific cases, but I strongly discourage you from using it, as it lacks the safety the `Match` method provides:

```CSharp
contactInfo.When<ContactInfo.Email>(email => { /* Some code */ });
```

## Records
A record is an aggregate of named values. Much like a tuple, record is immutable, and used to bind some values together. Infact, records and tuples are so alike, that if C# allowed to extend tuples, records would not be a part of this library.

Records implement `IEquatable<T>` interface, and have custom `GetHashCode` and `Equals`, along with `==` and `!=` operators, that are aware of the defined field and properties in the derived class.

Record is defined by extending from `Record<T>` class:

```CSharp
public class Money : Record<Money>
{
    public decimal Amount { get; }
    public Currency Currency { get; }

    public Money(decimal amount, Currency currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public override Result Validate()
    {
        if (Amount < 0)
            return new Result.Error(
                new ErrorResult("Money can't have negative amount.", ("Passed amount", Amount)));
        
        return new Result.Ok();
    }
}
```

_Note: Be sure to make all member immutable._

The record can be compared with other records and two records are equal as long as their members are equal. The comparison takes `IEquatable<T>` interface into account. Readonly fields can be used instead of properties.

The `Validate` method is optional. It returnes the built in Result union. Unfortunately, since C# calls for parent constructor before executing current constructor, it is not possible to call the method after the values are set. Instead the `Validated` union can be used:

```CSharp
public void Deposit(Validated<Money> money)
{
    money.Match(
        valid => { /* Success code */ },
        invalid => { /* Fail code */ });
}
```

The invalid case contains `RecordValidationError` that has the record and error message, explaining why the validation has failed. The valid case is a `Valid<T>` class. It has an internal constructor, so can not be created outside of this library, thus be used as validation enforcer:

```CSharp
public class Account : Record<Account>
{
    public Valid<Money> Money { get; }
    
    // Rest of the record
}
```

Since the records are immutable, they are modified with the `Copy` method, which allows to easily modify only the modified bits:

```CSharp
var newMoney = money.Copy(x =>
    x.With(m => m.Amount, money.Amount + 10));
// Or
var newMoney = money.Copy(x =>
    x.With(m => m.Amount, m => m.Amount + 10));
```

The second overload is helpful when you need to modify a nested record:

```CSharp
var modifiedRecord = record.Copy(x1 =>
    x1.With(r1 => r1.nestedRecord, r1.nestedRecord.Copy(x2 =>
        x2.With(r2 => r2.someValue, 42))));
```