// <copyright file="PaginationException.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.PaginationByClientRequest.Exceptions
{
    using System;

    public class PaginationException : Exception
    {
        public PaginationException(string message)
            : base(message)
        {
        }
    }
}
