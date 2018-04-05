// <copyright file="SummaryConverter.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace OperatorsToRst.Xml
{
    using System;
    using System.Text.RegularExpressions;
    using System.Xml.Linq;

    public class SummaryConverter
    {
        private readonly Regex para = new Regex("(<para>|</para>)");
        private readonly Regex c = new Regex("<c>([^<]+)</c>");
        private readonly Regex see = new Regex("<see cref=\"([^\"]+)\"[ ]?/>");
        private readonly Regex systemNewLines = new Regex($"{Environment.NewLine}[ ]*");
        private readonly Regex multipleNewLines = new Regex("[\n]{2,}");

        public string Convert(XElement element)
        {
            var summary = string.Concat(element.Nodes());
            summary = this.para.Replace(summary, m => "\n\n");
            summary = this.c.Replace(summary, m => $":code:`{m.Groups[1].Value}`");
            summary = this.see.Replace(summary, ToCodeSymbol);
            summary = this.systemNewLines.Replace(summary, m => "\n");
            return this.multipleNewLines.Replace(summary, m => "\n\n").Trim();
        }

        private static string ToCodeSymbol(Match match)
        {
            var symbol = match.Groups[1].Value;
            symbol = symbol.Substring(symbol.LastIndexOf('.') + 1);
            symbol = symbol.Replace("`1", "<TSource>");
            return $":code:`{symbol}`";
        }
    }
}
