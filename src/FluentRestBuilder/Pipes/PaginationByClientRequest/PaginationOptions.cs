// <copyright file="PaginationOptions.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.PaginationByClientRequest
{
    public class PaginationOptions
    {
        public int DefaultLimit { get; set; } = 10;

        public int MaxLimit { get; set; } = 100;
    }
}
