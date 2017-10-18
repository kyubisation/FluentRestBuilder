// <copyright file="TextFilters.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace OperatorsToRst.Templating
{
    public static class TextFilters
    {
        public static string WrapSignature(string signature)
        {
            var index = signature.IndexOf('(');
            return signature.Insert(index + 1, "\n" + new string(' ', 8));
        }
    }
}
