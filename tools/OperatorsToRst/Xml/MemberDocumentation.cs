// <copyright file="MemberDocumentation.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace OperatorsToRst.Xml
{
    using System.Xml.Linq;

    public class MemberDocumentation
    {
        private static readonly SummaryConverter Converter = new SummaryConverter();

        public MemberDocumentation(XElement element)
        {
            this.Summary = Converter.Convert(element.Element("summary"));
        }

        public string Summary { get; }
    }
}
