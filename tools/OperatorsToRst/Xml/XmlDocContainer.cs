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

    public class XmlDocContainer
    {
        private readonly Dictionary<Assembly, XElement> files;

        public XmlDocContainer(OperatorMethods operatorMethods)
        {
            this.files = operatorMethods
                .Select(g => g.First().DeclaringType.Assembly)
                .Distinct()
                .ToDictionary(a => a, LoadXmlFile);
        }

        public MemberDocumentation MemberDocumentation(MethodInfo method)
        {
            var methodString = new XmlMethodNameBuilder(method).Name;
            var documentationElement = this.files[method.DeclaringType.Assembly]
                .Element("members")?
                .Elements("member")
                .FirstOrDefault(e => e.Attribute("name")?.Value == methodString);
            if (documentationElement == null)
            {
                throw new InvalidOperationException();
            }

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
