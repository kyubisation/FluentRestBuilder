// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRest
{
    using Core;
    using EntityFrameworkCore.Pipes.Insertion;
    using Microsoft.Extensions.DependencyInjection;

    public static partial class Integration
    {
        public static EntityInsertionPipe<TInput> InsertEntity<TInput>(
            this IOutputPipe<TInput> pipe)
            where TInput : class =>
            pipe.GetRequiredService<IEntityInsertionPipeFactory<TInput>>()
                .Resolve(pipe);
    }
}
