// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Linq;
    using Hal;
    using Microsoft.Extensions.DependencyInjection;
    using Pipes.CollectionMapping;
    using Transformers;

    public static partial class Integration
    {
        public static OutputPipe<RestEntityCollection> MapToRestCollection<TInput, TOutput>(
            this IOutputPipe<IQueryable<TInput>> pipe, Func<TInput, TOutput> transformation)
            where TInput : class
            where TOutput : class =>
            pipe.GetRequiredService<ICollectionMappingPipeFactory<TInput, TOutput>>()
                .Resolve(transformation, pipe);

        public static OutputPipe<RestEntityCollection> UseTransformerForCollection<TInput, TOutput>(
            this IOutputPipe<IQueryable<TInput>> pipe,
            Func<ITransformerFactory<TInput>, ITransformer<TInput, TOutput>> selection)
            where TInput : class
            where TOutput : class
        {
            var transformer = pipe.GetService<ITransformerFactory<TInput>>();
            return pipe.GetRequiredService<ICollectionMappingPipeFactory<TInput, TOutput>>()
                .Resolve(i => selection(transformer).Transform(i), pipe);
        }

        public static OutputPipe<RestEntityCollection> BuildTransformationForCollection<TInput, TOutput>(
            this IOutputPipe<IQueryable<TInput>> pipe,
            Func<ITransformationBuilder<TInput>, Func<TInput, TOutput>> builder)
            where TInput : class
            where TOutput : class
        {
            var transformerBuilder = pipe.GetService<ITransformationBuilder<TInput>>();
            return pipe.GetRequiredService<ICollectionMappingPipeFactory<TInput, TOutput>>()
                .Resolve(builder(transformerBuilder), pipe);
        }
    }
}
