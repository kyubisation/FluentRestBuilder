// <copyright file="IEntityCollectionSourcePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.ChainPipes.EntityCollectionSource
{
    using System;
    using System.Linq;
    using Core.Common;

    public interface IEntityCollectionSourcePipeFactory<TInput, TOutput>
        where TOutput : class
    {
        EntityCollectionSourcePipe<TInput, TOutput> Resolve(
            Func<IQueryableFactory, IQueryable<TOutput>> selection,
            Func<IQueryable<TOutput>, TInput, IQueryable<TOutput>> queryablePipe,
            IOutputPipe<TInput> pipe);
    }
}
