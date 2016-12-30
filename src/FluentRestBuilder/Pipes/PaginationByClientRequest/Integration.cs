// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System.Linq;
    using Microsoft.Extensions.DependencyInjection;
    using Pipes.PaginationByClientRequest;

    public static partial class Integration
    {
        public static OutputPipe<IQueryable<TInput>> ApplyPaginationByClientRequest<TInput>(
                this IOutputPipe<IQueryable<TInput>> pipe,
                PaginationOptions options = null) =>
            pipe.GetService<IPaginationByClientRequestPipeFactory<TInput>>()
                .Resolve(options, pipe);
    }
}
