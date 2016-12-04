// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRest
{
    using System;
    using System.Linq;
    using Core;
    using Core.ChainPipes.EntityCollectionSource;
    using Core.Common;
    using Microsoft.Extensions.DependencyInjection;

    public static partial class Integration
    {
        public static EntityCollectionSourcePipe<TInput, TOutput> CreateEntityCollectionSource<TInput, TOutput>(
            this IOutputPipe<TInput> pipe,
            Func<IQueryableFactory, IQueryable<TOutput>> selection,
            Func<IQueryable<TOutput>, TInput, IQueryable<TOutput>> queryablePipe = null)
            where TOutput : class =>
            pipe.GetRequiredService<IEntityCollectionSourcePipeFactory<TInput, TOutput>>()
                .Resolve(selection, queryablePipe, pipe);
    }
}
