// <copyright file="SummaryConverter.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace OperatorsToRst.Xml
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml.Linq;

    public class SummaryConverter
    {
        private static readonly Regex Para = new Regex("(<para>|</para>)");
        private static readonly Regex C = new Regex("<c>([^<]+)</c>");
        private static readonly Regex See = new Regex("<see cref=\"([^\"]+)\"[ ]?/>");
        private static readonly Regex SystemNewLines = new Regex($"{Environment.NewLine}[ ]*");
        private static readonly Regex MultipleNewLines = new Regex("[\n]{2,}");

        public string Convert(XElement element) =>
            new Func<string, string>[]
            {
                s => Para.Replace(s, m => "\n\n"),
                s => C.Replace(s, m => $":code:`{m.Groups[1].Value}`"),
                s => See.Replace(s, ToCodeSymbol),
                s => SystemNewLines.Replace(s, m => "\n"),
                s => MultipleNewLines.Replace(s, m => "\n\n"),
                s => s.Trim(),
            }.Aggregate(string.Concat(element.Nodes()), (current, next) => next(current));

        private static string ToCodeSymbol(Match match) =>
            new Func<string, string>[]
            {
                s => s.Substring(s.LastIndexOf('.') + 1),
                s => s.Replace("`1", "<TSource>"),
                s => $":code:`{s}`",
            }.Aggregate(match.Groups[1].Value, (current, next) => next(current));
    }
}
