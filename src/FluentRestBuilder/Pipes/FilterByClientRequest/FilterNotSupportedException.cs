// <copyright file="FilterNotSupportedException.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest
{
    public class FilterNotSupportedException : FilterException
    {
        public FilterNotSupportedException(string property)
            : base($"'{property}' filter is not supported!")
        {
        }
    }
}
