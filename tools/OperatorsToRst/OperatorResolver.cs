// <copyright file="OperatorResolver.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace OperatorsToRst
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Models;
    using Xml;

    public class OperatorResolver
    {
        private readonly OperatorMethods operatorMethods;
        private readonly XmlDocContainer xmlContainer;

        public OperatorResolver(
            OperatorMethods operatorMethods,
            XmlDocContainer xmlContainer)
        {
            this.operatorMethods = operatorMethods;
            this.xmlContainer = xmlContainer;
            this.xmlContainer = new XmlDocContainer(this.operatorMethods);
        }

        public IReadOnlyDictionary<string, OperatorDocumentation> Resolve() =>
            this.operatorMethods.ToDictionary(m => m.Key, this.Create);

        private OperatorDocumentation Create(IGrouping<string, MethodInfo> methods)
        {
            var variants = methods.Select(this.CreateVariantDocumentation)
                .ToList();
            return new OperatorDocumentation(methods.Key, variants);
        }

        private OperatorVariantDocumentation CreateVariantDocumentation(MethodInfo method)
        {
            var memberDocumentation = this.xmlContainer.MemberDocumentation(method);
            return new OperatorVariantDocumentation(method, memberDocumentation);
        }
    }
}
