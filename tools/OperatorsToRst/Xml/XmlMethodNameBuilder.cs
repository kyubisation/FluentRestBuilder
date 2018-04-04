// <copyright file="XmlMethodNameBuilder.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace OperatorsToRst.Xml
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;

    public class XmlMethodNameBuilder
    {
        private static readonly Regex GenericSuffix = new Regex("`\\d+$");
        private readonly List<string> genericParameters;

        public XmlMethodNameBuilder(MethodInfo method)
        {
            this.genericParameters = method.GetGenericArguments()
                .Select(p => p.Name)
                .ToList();
            var builder = new StringBuilder(
                $"M:{method.DeclaringType.FullName}.{method.Name}");
            builder.Append($"``{this.genericParameters.Count}(");
            var parameters = method.GetParameters()
                .Select(p => this.BuildType(p.ParameterType));
            builder.AppendJoin(",", parameters);
            builder.Append(")");
            this.Name = builder.ToString();
        }

        public string Name { get; }

        private string BuildType(Type type)
        {
            if (!type.GetGenericArguments().Any() || type.IsGenericParameter)
            {
                return this.FullName(type);
            }

            var builder = new StringBuilder(this.FullName(type));
            builder.Append("{");
            var genericArguments = type.GetGenericArguments()
                .Select(this.BuildType);
            builder.AppendJoin(",", genericArguments);
            builder.Append("}");

            return builder.ToString();
        }

        private string FullName(Type type) =>
            this.genericParameters.Contains(type.Name)
                ? $"``{this.genericParameters.IndexOf(type.Name)}"
                : $"{type.Namespace}.{GenericSuffix.Replace(type.Name, string.Empty)}";
    }
}
