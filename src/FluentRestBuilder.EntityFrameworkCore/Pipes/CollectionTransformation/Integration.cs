// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Linq;
    using EntityFrameworkCore.Pipes.CollectionTransformation;
    using Microsoft.Extensions.DependencyInjection;
    using Transformers;

    public static partial class Integration
    {
        public static OutputPipe<RestEntityCollection> TransformCollection<TInput, TOutput>(
            this IOutputPipe<IQueryable<TInput>> pipe, Func<TInput, TOutput> transformation)
            where TInput : class
            where TOutput : class =>
            pipe.GetRequiredService<ICollectionTransformationPipeFactory<TInput, TOutput>>()
                .Resolve(transformation, pipe);

        public static OutputPipe<RestEntityCollection> UseTransformerForCollection<TInput, TOutput>(
            this IOutputPipe<IQueryable<TInput>> pipe,
            Func<ITransformerFactory<TInput>, ITransformer<TInput, TOutput>> selection)
            where TInput : class
            where TOutput : class =>
            pipe.GetRequiredService<ICollectionTransformationPipeFactory<TInput, TOutput>>()
                .ResolveTransformer(selection, pipe);

        public static OutputPipe<RestEntityCollection> BuildTransformationForCollection<TInput, TOutput>(
            this IOutputPipe<IQueryable<TInput>> pipe,
            Func<ITransformationBuilder<TInput>, Func<TInput, TOutput>> builder)
            where TInput : class
            where TOutput : class =>
            pipe.GetRequiredService<ICollectionTransformationPipeFactory<TInput, TOutput>>()
                .ResolveTransformationBuilder(builder, pipe);
    }
}
