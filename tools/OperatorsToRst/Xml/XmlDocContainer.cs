// <copyright file="XmlDocContainer.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace OperatorsToRst.Xml
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Xml.Linq;
    using Extensions;

    public class XmlDocContainer
    {
        private readonly Dictionary<Assembly, XElement> files;

        public XmlDocContainer(IEnumerable<IGrouping<string, MethodInfo>> operatorMethods)
        {
            this.files = operatorMethods
                .Select(g => g.First().DeclaringType.Assembly)
                .Distinct()
                .ToDictionary(a => a, LoadXmlFile);
        }

        public MemberDocumentation MemberDocumentation(MethodInfo method)
        {
            var methodName = $"M:{method.DeclaringType.FullName}.{method.Name}";
            var methodString = method.ToString();
            var documentationElement = this.files[method.DeclaringType.Assembly]
                    .Element("members")?
                    .Elements("member").Where(e => e.Attribute("name")?.Value.StartsWith(methodName) == true)
                .OrderBy(e => methodString.ComputeDistance(e.Attribute("name")?.Value))
                .First();
            return new MemberDocumentation(documentationElement);
        }

        private static XElement LoadXmlFile(Assembly assembly)
        {
            var xmlDir = Path.GetDirectoryName(assembly.Location);
            var xmlFile = Path.GetFileNameWithoutExtension(assembly.Location) + ".xml";
            var xmlFilePath = Path.Combine(xmlDir, xmlFile);
            if (!File.Exists(xmlFilePath))
            {
                throw new InvalidOperationException($"Missing XML file: '{xmlFilePath}'");
            }

            return XElement.Load(xmlFilePath);
        }
    }
}
