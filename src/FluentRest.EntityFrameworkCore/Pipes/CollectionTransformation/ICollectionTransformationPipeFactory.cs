// <copyright file="ICollectionTransformationPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.Pipes.CollectionTransformation
{
    using System;
    using System.Linq;
    using Transformers;

    public interface ICollectionTransformationPipeFactory<TInput, TOutput>
    {
        CollectionTransformationPipe<TInput, TOutput> Resolve(
            Func<TInput, TOutput> transformation, IOutputPipe<IQueryable<TInput>> parent);

        CollectionTransformationPipe<TInput, TOutput> ResolveTransformer(
            Func<ITransformerFactory<TInput>, ITransformer<TInput, TOutput>> selection,
            IOutputPipe<IQueryable<TInput>> parent);

        CollectionTransformationPipe<TInput, TOutput> ResolveTransformationBuilder(
            Func<ITransformationBuilder<TInput>, Func<TInput, TOutput>> builder,
            IOutputPipe<IQueryable<TInput>> parent);
    }
}
