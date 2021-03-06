﻿// <copyright file="OrderByRequest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators.ClientRequest.Interpreters.Requests
{
    public class OrderByRequest
    {
        public OrderByRequest(string originalProperty, string property, OrderByDirection direction)
        {
            this.OriginalProperty = originalProperty;
            this.Property = property;
            this.Direction = direction;
        }

        public OrderByRequest(string property, OrderByDirection direction)
            : this(property, property, direction)
        {
        }

        public string OriginalProperty { get; }

        public string Property { get; }

        public OrderByDirection Direction { get; }

        public override string ToString() => Stringifier.Convert(this);
    }
}
