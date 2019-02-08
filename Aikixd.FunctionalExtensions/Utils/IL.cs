using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Aikixd.FunctionalExtensions.DiscriminatedUnions;
using System.Reflection.Emit;
using System.Runtime.Serialization;

namespace Aikixd.FunctionalExtensions.Utils
{
    internal class IL
    {
        public static Func<T, T, bool> GenerateFieldsCompare<T>()
        {
            var paramA = Expression.Parameter(typeof(T), "left");
            var paramB = Expression.Parameter(typeof(T), "right");

            var returnLabel = Expression.Label(typeof(bool), "return");

            var comparisons =
                GetFields<T>()
                .Select(fieldInfo =>
                    // For equality comparers should call the x.Equals(x, y).
                    // For the rest the default equality comparison is used.
                    typeof(IEquatable<>)
                        .MakeGenericType(fieldInfo.FieldType)
                        .IsAssignableFrom(fieldInfo.FieldType) 
                        ?
                    (Expression)
                    Expression.Not(
                        Expression.Call(
                            Expression.Field(paramA, fieldInfo),
                            fieldInfo.FieldType.GetMethod("Equals", new[] { fieldInfo.FieldType }),
                            Expression.Field(paramB, fieldInfo)
                        )) :

                    Expression.NotEqual(
                        Expression.Field(paramA, fieldInfo),
                        Expression.Field(paramB, fieldInfo)
                        ))
                .Select(e =>
                    // if (e)
                    //     return false;
                    (Expression)Expression.IfThen(
                        e,
                        Expression.Return(returnLabel, Expression.Constant(false, typeof(bool)))))
                .Union(
                    // return true;
                    new[] { Expression.Label(returnLabel, Expression.Constant(true, typeof(bool))) });
            
            var equals = Expression.Block(
                comparisons                
                );

            return Expression.Lambda<Func<T, T, bool>>(equals, paramA, paramB).Compile();
        }

        public static Func<T, int> GenerateGetHashCode<T>()
        {
            var param = Expression.Parameter(typeof(T), "obj");
            var hash = Expression.Variable(typeof(int), "hash");            

            var computation =
                GetFields<T>()
                .Select(fieldInfo =>
                    Expression.AddAssign(hash, 
                        Expression.Call(
                            Expression.Field(param, fieldInfo), 
                            typeof(object).GetMethod("GetHashCode", new Type[] { }))));

            var block =
                Expression
                .Block(
                    new[] { hash },
                    computation
                    );

            return Expression.Lambda<Func<T, int>>(block, param).Compile();
        }

        public static Func<T, int, string> GenerateToString<T>()
        {
            var param  = Expression.Parameter(typeof(T), "obj");
            var indent = Expression.Parameter(typeof(int), "indentation");
            var array  = Expression.Parameter(typeof(string[]), "array");

            var stringConcat2 = 
                typeof(string).GetMethod("Concat", new Type[] {
                    typeof(object),
                    typeof(object)
                });

            var stringConcat3 =
                typeof(string).GetMethod("Concat", new Type[] {
                    typeof(object),
                    typeof(object),
                    typeof(object)
                });

            var stringJoin =
                typeof(string).GetMethod("Join", new[] { typeof(string), typeof(string[]) });

            var lineIndent = ((Func<int, string>)IlHelpers.GetRecordLineIndent).Method;

            var getStrings =
                GetFields<T>()
                .Select((fieldInfo, i) =>
                    // String concat
                    StringConcat2(
                        // "Name: "
                        Expression.Constant(getFieldName(fieldInfo) + ": ", typeof(string)),
                        // And the to string result
                        getToStringCall(fieldInfo)
                        ));

            var block = Expression.Block(
                new[] { array },
                Expression.Assign(array, Expression.NewArrayInit(typeof(string), getStrings)),
                Expression.Call(
                    stringConcat3,
                    StringConcat2(
                        Expression.Constant("{\n"),
                        Expression.Call(lineIndent, indent)
                        ),
                    Expression.Call(
                        stringJoin, 
                        StringConcat2(
                            Expression.Constant(",\n"),
                            Expression.Call(lineIndent, indent)),
                        array),
                    StringConcat3(
                        Expression.Constant("\n"),
                        Expression.Call(lineIndent, Expression.Subtract(indent, Expression.Constant(1, typeof(int)))),
                        Expression.Constant("}", typeof(string))
                        )
                    ));


            return Expression.Lambda<Func<T, int, string>>(block, param, indent).Compile();

            /***** Nested methods *****/

            string getFieldName(FieldInfo nfo)
            {
                if (nfo.CustomAttributes.Any(x => x.AttributeType == typeof(CompilerGeneratedAttribute)))
                    return IlHelpers.StripBakingFieldName(nfo.Name);

                return nfo.Name;
            }

            Expression getToStringCall(FieldInfo nfo)
            {
                var prettyToString = typeof(object).GetMethod("ToString", new[] { typeof(int) });

                if (prettyToString != null)
                    return Expression.Call(
                        Expression.Field(param, nfo),
                        prettyToString,
                        Expression.Add(indent, Expression.Constant(1)));

                return Expression.Call(
                    Expression.Field(param, nfo),
                    typeof(object).GetMethod("ToString", new Type[] { }));
            }

            Expression StringConcat2(Expression a, Expression b)
            {
                return Expression.Call(
                    stringConcat2,
                    a,
                    b);
            }

            Expression StringConcat3(Expression a, Expression b, Expression c)
            {
                return Expression.Call(
                    stringConcat3,
                    a,
                    b,
                    c);
            }
        }

        internal static Func<T, T> GenerateRecordCopy<T>()
            where T : class
        {
            var source = Expression.Parameter(typeof(T), "source");
            var target = Expression.Variable(typeof(T), "target");

            var returnLabel = Expression.Label(typeof(T));

            var constr = 
                typeof(T)
                .GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .First();

            var constrArgs =
                constr
                .GetParameters()
                .Select(x =>
                    new
                    {
                        type = x.ParameterType,
                        val = x.ParameterType.IsValueType ?
                            Activator.CreateInstance(x.ParameterType) :
                            null
                    })
                .Select(x => Expression.Constant(x.val, x.type));

            var constrCall = Expression.New(constr, constrArgs);
            var createObj = 
                Expression.Convert(
                    Expression.Call(
                        ((Func<Type, object>)FormatterServices.GetUninitializedObject).Method,
                        Expression.Constant(typeof(T), typeof(Type))),
                    typeof(T));

            var assignments =
                GetFields<T>()
                .Select(fieldInfo => MakeFieldCopy(source, target, fieldInfo));

            var body =
                new[] { (Expression)Expression.Assign(target, createObj) }
                .Concat(assignments)
                .Concat(new[] {
                    // This will fail if TRecord is not directly derived from Record
                    MakeFieldCopy(source, target, typeof(T).BaseType.GetField("utils", 
                                                                                      BindingFlags.NonPublic | BindingFlags.Instance)),
                    Expression.Return(returnLabel, target),
                    Expression.Label(returnLabel, Expression.Default(typeof(T)))
                });

            var block = Expression.Block(
                new[] { target },
                body);

            return Expression.Lambda<Func<T, T>>(block, source).Compile();

            
                
        }

        internal static IReadOnlyDictionary<MemberInfo, Action<T, object>> GenerateRecordFieldsSetMap<T>()
        {
            var (props, allFields) =
                GetMembers<T>()
                .Where(x => x.MemberType == MemberTypes.Field || x.MemberType == MemberTypes.Property)
                .Partition(x => x.MemberType == MemberTypes.Property)
                .Do(x => (
                    x.trues.Cast<PropertyInfo>().ToArray(), 
                    x.falses.Cast<FieldInfo>().ToArray()));

            var (autoFields, realFields) =
                allFields
                .Partition(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(CompilerGeneratedAttribute)))
                .Do(x => (x.trues.ToArray(), x.falses.ToArray()));

            var autoProps =
                autoFields
                .Join(
                    props,
                    x => IlHelpers.StripBakingFieldName(x.Name),
                    x => x.Name,
                    (x, y) => (prop: y, field: x))
                .ToArray();

            var realProps =
                props
                .Except(autoProps.Select(x => x.prop))
                .Where(x => x.CanWrite)
                .ToArray();

            var value = Expression.Parameter(typeof(object), "value");
            var target = Expression.Parameter(typeof(T), "target");

            var realFieldAssignments =
                realFields
                .Select(x => 
                    (member: (MemberInfo)x,
                    expression: (Expression)MakeFieldAssign(target, value, x)));

            var autoPropsAssignments =
                autoProps
                .Select(x => 
                    (member: (MemberInfo)x.prop,
                    expression: (Expression)MakeFieldAssign(target, value, x.field)));

            var realPropsAssignments =
                realProps
                .Select(x =>
                    (member: (MemberInfo)x,
                    expression: (Expression)Expression.Assign(
                        Expression.Property(target, x.SetMethod),
                        value)));

            return
                realFieldAssignments
                .Union(autoPropsAssignments)
                .Union(realPropsAssignments)
                .ToDictionary(
                    x => x.member,
                    x => Expression.Lambda<Action<T, object>>(x.expression, target, value).Compile());
        }

        internal static Func<TCase, TUnion> GenerateCaseCast<TUnion, TCase>(Type caseType)
            where TCase : Case<TUnion>
        {
            var constr = typeof(TUnion).GetConstructor(new[] { caseType });

            if (constr == null)
                throw new InvalidUnionDefinitionException($"Union of type {typeof(TUnion).Namespace}.{typeof(TUnion).Name} lack constructor for case of type {caseType.Namespace}.{caseType.Name}. Ensure that the union has constructor for each possible case.");

            var @case = Expression.Parameter(typeof(TCase), "case");

            var constructorCall =
                Expression.New(
                    constr, 
                    Expression.Convert(@case, caseType));

            return Expression.Lambda<Func<TCase, TUnion>>(constructorCall, @case).Compile();
        }

        public static TypeInfo GenerateRecordProxy<T>()
        {
            var recordType = typeof(T);

            var domain = AppDomain.CurrentDomain;

            var assemblyName = $"<{recordType.Namespace}.{recordType.Name}>ProxyAssembly";
            var assembly = AssemblyBuilder.DefineDynamicAssembly(
                new AssemblyName(assemblyName),
                AssemblyBuilderAccess.Run);

            var module = assembly.DefineDynamicModule(assemblyName);

            var type = module.DefineType(
                $"<{recordType.Namespace}.{recordType.Name}>Proxy",
                TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.AnsiClass | TypeAttributes.AutoClass,
                recordType
                );

            var constructor = type.DefineConstructor(
                MethodAttributes.Public |
                MethodAttributes.HideBySig |
                MethodAttributes.RTSpecialName |
                MethodAttributes.SpecialName,
                CallingConventions.Standard,
                new[] { recordType });

            EmitObjectCopy<T>(constructor.GetILGenerator());

            return type.CreateTypeInfo();
        }

        public static Func<T, T> GenerateCopyConstructorCall<T>(ConstructorInfo info)
        {
            var source = Expression.Parameter(typeof(T), "source");

            var constructorCall =
                Expression.New(
                    info,
                    source);

            return Expression.Lambda<Func<T, T>>(constructorCall, source).Compile();
        }

        private static Expression MakeFieldCopy(ParameterExpression source, ParameterExpression target, FieldInfo nfo)
        {
            return
                MakeFieldAssign(target, Expression.Field(source, nfo), nfo);
        }

        private static Expression MakeFieldAssign(ParameterExpression target, Expression value, FieldInfo nfo)
        {
            return
                nfo.IsInitOnly ?
                MakeForceAssign(target, value, nfo) :
                Expression.Assign(
                    Expression.Field(target, nfo),
                    value
                    );
        }

        private static Expression MakeForceAssign(ParameterExpression target, Expression value, FieldInfo nfo)
        {
            return Expression.Call(
                ((Action<object, object, FieldInfo>)
                IlHelpers.ForceAssign).Method,
                target,
                value.Type.IsValueType ? Expression.Convert(value, typeof(object)) : value,
                Expression.Constant(nfo));
        }

        private static FieldInfo[] GetFields<T>()
        {
            var flags = 
                BindingFlags.Instance | 
                BindingFlags.Public | 
                BindingFlags.NonPublic;

            return typeof(T)
                .GetFields(flags);
        }

        private static MemberInfo[] GetMembers<T>()
        {
            var flags =
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic;

            return typeof(T)
                .GetMembers(flags);
        }

        private static void EmitObjectCopy<T>(ILGenerator il)
        {
            var fields = GetFields<T>();

            foreach (var field in fields)
            {
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldarg_1);
                il.Emit(OpCodes.Ldfld, field);
                il.Emit(OpCodes.Stfld, field);
            }

            il.Emit(OpCodes.Ret);
        }
    }
}
