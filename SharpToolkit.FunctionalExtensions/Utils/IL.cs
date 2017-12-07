using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using SharpToolkit.FunctionalExtensions.DiscriminatedUnions;

namespace SharpToolkit.FunctionalExtensions.Utils
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
                    typeof(IEqualityComparer<>)
                        .MakeGenericType(fieldInfo.FieldType)
                        .IsAssignableFrom(fieldInfo.FieldType) 
                        ?
                    (Expression)
                    Expression.Not(
                        Expression.Call(
                            Expression.Field(paramA, fieldInfo),
                            fieldInfo.FieldType.GetMethod("Equals", new[] { fieldInfo.FieldType, fieldInfo.FieldType }),
                            Expression.Field(paramA, fieldInfo),
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
                            fieldInfo.FieldType.GetMethod("GetHashCode", new Type[] { }))));

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

            var lineIndent = ((Func<int, string>)IlHelpers.getRecordLineIndent).Method;

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
                    return nfo.Name.Substring(1, nfo.Name.IndexOf('>') - 1);

                return nfo.Name;
            }

            Expression getToStringCall(FieldInfo nfo)
            {
                var prettyToString = nfo.FieldType.GetMethod("ToString", new[] { typeof(int) });

                if (prettyToString != null)
                    return Expression.Call(
                        Expression.Field(param, nfo),
                        prettyToString,
                        Expression.Add(indent, Expression.Constant(1)));

                return Expression.Call(
                    Expression.Field(param, nfo),
                    nfo.FieldType.GetMethod("ToString", new Type[] { }));
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

        private static FieldInfo[] GetFields<T>()
        {
            var flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

            return typeof(T)
                .GetFields(flags);
        }
    }
}
