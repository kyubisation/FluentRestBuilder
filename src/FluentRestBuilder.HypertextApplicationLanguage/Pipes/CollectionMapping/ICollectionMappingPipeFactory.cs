// <copyright file="ICollectionMappingPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage.Pipes.CollectionMapping
{
    using System;
    using System.Linq;
    using HypertextApplicationLanguage;

    public interface ICollectionMappingPipeFactory<TInput, in TOutput>
    {
        OutputPipe<RestEntityCollection> Resolve(
            Func<TInput, TOutput> mapping, IOutputPipe<IQueryable<TInput>> parent);
    }
}
