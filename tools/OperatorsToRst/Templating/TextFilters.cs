// <copyright file="TextFilters.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace OperatorsToRst.Templating
{
    using System.Text.RegularExpressions;

    public static class TextFilters
    {
        private static readonly Regex ParameterSeparator =
            new Regex("([>a-zA-Z0-9]) [a-zA-Z0-9]+, ");

        public static string WrapSignature(string signature)
        {
            var newlineWhitespace = $"\n{new string(' ', 8)}";
            signature = signature.Insert(signature.IndexOf('(') + 1, newlineWhitespace);
            return ParameterSeparator.Replace(
                signature, m => $"{m.Value.Trim()}{newlineWhitespace}");
        }
    }
}
