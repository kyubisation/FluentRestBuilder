// <copyright file="OrderByNotSupportedException.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.OrderByClientRequest
{
    using System;

    public class OrderByNotSupportedException : Exception
    {
        public OrderByNotSupportedException(string property)
            : base($"Ordering by '{property}' is not supported!")
        {
        }
    }
}
