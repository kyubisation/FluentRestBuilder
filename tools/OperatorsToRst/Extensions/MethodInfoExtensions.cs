// <copyright file="MethodInfoExtensions.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace OperatorsToRst.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Text;

    public static class MethodInfoExtensions
    {
        /// <summary>
        /// Return the method signature as a string.
        /// </summary>
        /// <param name="method">The Method</param>
        /// <param name="callable">Return as an callable string(public void a(string b) would return a(b))</param>
        /// <returns>Method signature</returns>
        public static string GetSignature(this MethodInfo method, bool callable = false)
        {
            var sigBuilder = new StringBuilder();
            if (callable == false)
            {
                sigBuilder.AppendJoin<string>(string.Empty, PrependCallablePart(method));
            }

            sigBuilder.Append(method.Name);

            // Add method generics
            if (method.IsGenericMethod)
            {
                var types = string.Join(", ", method.GetGenericArguments().Select(TypeName));
                sigBuilder.Append($"<{types}>");
            }

            sigBuilder.Append("(");
            var firstParam = true;
            var secondParam = false;
            foreach (var param in method.GetParameters())
            {
                if (firstParam)
                {
                    firstParam = false;
                    if (method.IsDefined(typeof(ExtensionAttribute), false))
                    {
                        if (callable)
                        {
                            secondParam = true;
                            continue;
                        }

                        sigBuilder.Append("this ");
                    }
                }
                else if (secondParam)
                {
                    secondParam = false;
                }
                else
                {
                    sigBuilder.Append(", ");
                }

                if (param.ParameterType.IsByRef)
                {
                    sigBuilder.Append("ref ");
                }
                else if (param.IsOut)
                {
                    sigBuilder.Append("out ");
                }

                if (!callable)
                {
                    sigBuilder.Append((string)TypeName(param.ParameterType));
                    sigBuilder.Append(' ');
                }

                sigBuilder.Append(param.Name);
            }

            sigBuilder.Append(")");
            return sigBuilder.ToString();
        }

        private static IEnumerable<string> PrependCallablePart(MethodInfo method)
        {
            if (method.IsPublic)
            {
                yield return "public ";
            }
            else if (method.IsPrivate)
            {
                yield return "private ";
            }
            else if (method.IsAssembly)
            {
                yield return "internal ";
            }

            if (method.IsFamily)
            {
                yield return "protected ";
            }

            if (method.IsStatic)
            {
                yield return "static ";
            }

            yield return $"{TypeName(method.ReturnType)} ";
        }

        /// <summary>
        /// Get full type name with full namespace names
        /// </summary>
        /// <param name="type">Type. May be generic or nullable</param>
        /// <returns>Full type name, fully qualified namespaces</returns>
        private static string TypeName(Type type)
        {
            var nullableType = Nullable.GetUnderlyingType(type);
            if (nullableType != null)
            {
                return nullableType.Name + "?";
            }

            if (!(type.IsGenericType && type.Name.Contains("`")))
            {
                switch (type.Name)
                {
                    case "String":
                        return "string";
                    case "Int32":
                        return "int";
                    case "Decimal":
                        return "decimal";
                    case "Object":
                        return "object";
                    case "Void":
                        return "void";
                    default:
                        return string.IsNullOrWhiteSpace(type.FullName) ? type.Name : type.FullName;
                }
            }

            var sb = new StringBuilder(type.Name.Substring(0, type.Name.IndexOf('`')));
            sb.Append('<');
            var first = true;
            foreach (var t in type.GetGenericArguments())
            {
                if (!first)
                {
                    sb.Append(',');
                }

                sb.Append((string)TypeName(t));
                first = false;
            }

            sb.Append('>');
            return sb.ToString();
        }
    }
}
