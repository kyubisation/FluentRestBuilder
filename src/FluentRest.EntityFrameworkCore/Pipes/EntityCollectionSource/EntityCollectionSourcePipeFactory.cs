// <copyright file="EntityCollectionSourcePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.EntityFrameworkCore.Pipes.EntityCollectionSource
{
    using System;
    using System.Linq;
    using Common;
    using Core;

    public class EntityCollectionSourcePipeFactory<TInput, TOutput> :
        IEntityCollectionSourcePipeFactory<TInput, TOutput>
        where TOutput : class
    {
        private readonly IQueryableFactory queryableFactory;

        public EntityCollectionSourcePipeFactory(IQueryableFactory queryableFactory)
        {
            this.queryableFactory = queryableFactory;
        }

        public EntityCollectionSourcePipe<TInput, TOutput> Resolve(
                Func<IQueryableFactory, IQueryable<TOutput>> selection,
                Func<IQueryable<TOutput>, TInput, IQueryable<TOutput>> queryablePipe,
                IOutputPipe<TInput> pipe) =>
            new EntityCollectionSourcePipe<TInput, TOutput>(
                queryablePipe, selection(this.queryableFactory), pipe);
    }
}