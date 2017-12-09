// <copyright file="OperatorDocumentationFile.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable MemberCanBePrivate.Local
// ReSharper disable UnusedAutoPropertyAccessor.Local
namespace OperatorsToRst
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using DotLiquid;
    using Extensions;
    using Templating;
    using Xml;

    public class OperatorDocumentationFile
    {
        private static readonly RstTemplate Template = new RstTemplate("operator");
        private readonly XmlDocContainer xmlDocs;
        private readonly List<MethodInfo> methodList;

        public OperatorDocumentationFile(IEnumerable<MethodInfo> methods, XmlDocContainer xmlDocs)
        {
            this.xmlDocs = xmlDocs;
            this.methodList = methods.ToList();
        }

        public string Content() =>
            Template.Render(new Documentations()
            {
                Name = this.methodList.First().Name,
                Variants = this.methodList
                    .Select(m => new Documentation(m, this.xmlDocs.MemberDocumentation(m)))
                    .ToList(),
            });

        private sealed class Documentations
        {
            public string Name { get; set; }

            public List<Documentation> Variants { get; set; }
        }

        [LiquidType(nameof(Package), nameof(Summary), nameof(MethodSignature))]
        private sealed class Documentation
        {
            public Documentation(MethodInfo method, MemberDocumentation documentation)
            {
                this.Package = method.DeclaringType.Assembly.GetName().Name;
                this.Summary = documentation.Summary;
                this.MethodSignature = method.GetSignature();
            }

            public string Package { get; }

            public string Summary { get; }

            public string MethodSignature { get; }
        }
    }
}
