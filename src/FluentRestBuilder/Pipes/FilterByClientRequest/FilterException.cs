// <copyright file="FilterException.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest
{
    using System;

    public class FilterException : Exception
    {
        public FilterException(string message)
            : base(message)
        {
        }
    }
}
