// <copyright file="PaginationOptions.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators.ClientRequest
{
    using System;

    public class PaginationOptions
    {
        public int DefaultLimit { get; set; } = 10;

        public int MaxLimit { get; set; } = 100;

        public void ThrowOnInvalidConfiguration()
        {
            if (this.MaxLimit < this.DefaultLimit)
            {
                throw new InvalidOperationException(
                    $"${nameof(this.MaxLimit)} must not be " +
                    $"smaller than ${nameof(this.DefaultLimit)}!");
            }
        }
    }
}
