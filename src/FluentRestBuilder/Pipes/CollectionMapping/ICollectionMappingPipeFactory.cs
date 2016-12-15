// <copyright file="ICollectionMappingPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.CollectionMapping
{
    using System;
    using System.Linq;
    using Hal;
    using Transformers;

    public interface ICollectionMappingPipeFactory<TInput, in TOutput>
    {
        OutputPipe<RestEntityCollection> Resolve(
            Func<TInput, TOutput> transformation, IOutputPipe<IQueryable<TInput>> parent);

        OutputPipe<RestEntityCollection> ResolveTransformer(
            Func<ITransformerFactory<TInput>, ITransformer<TInput, TOutput>> selection,
            IOutputPipe<IQueryable<TInput>> parent);

        OutputPipe<RestEntityCollection> ResolveTransformationBuilder(
            Func<ITransformationBuilder<TInput>, Func<TInput, TOutput>> builder,
            IOutputPipe<IQueryable<TInput>> parent);
    }
}
