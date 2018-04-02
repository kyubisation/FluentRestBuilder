// <copyright file="OperatorVariantDocumentation.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace OperatorsToRst.Models
{
    using System.Reflection;
    using System.Text.RegularExpressions;
    using Extensions;
    using Xml;

    public class OperatorVariantDocumentation
    {
        private static readonly Regex ParameterSeparator =
            new Regex("([>a-zA-Z0-9]) [a-zA-Z0-9]+, ");

        public OperatorVariantDocumentation(MethodInfo method, MemberDocumentation documentation)
        {
            this.Package = method.DeclaringType.Assembly.GetName().Name;
            this.Summary = documentation.Summary;
            this.MethodSignature = method.GetSignature();
        }

        public string Package { get; }

        public string Summary { get; }

        public string MethodSignature { get; }

        public string WrappedMethodSignature => this.WrapMethodSignature();

        private string WrapMethodSignature()
        {
            var newlineWhitespace = $"\n{new string(' ', 8)}";
            var signature = this.MethodSignature
                .Insert(this.MethodSignature.IndexOf('(') + 1, newlineWhitespace);
            return ParameterSeparator.Replace(
                signature, m => $"{m.Value.Trim()}{newlineWhitespace}");
        }
    }
}
