// <copyright file="ICollectionMappingPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.CollectionMapping
{
    using System;
    using System.Linq;
    using Hal;

    public interface ICollectionMappingPipeFactory<TInput, in TOutput>
    {
        OutputPipe<RestEntityCollection> Resolve(
            Func<TInput, TOutput> mapping, IOutputPipe<IQueryable<TInput>> parent);
    }
}
