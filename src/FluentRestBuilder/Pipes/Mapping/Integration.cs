// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Pipes.Mapping;
    using Transformers;

    public static partial class Integration
    {
        public static OutputPipe<TOutput> Map<TInput, TOutput>(
            this IOutputPipe<TInput> pipe, Func<TInput, TOutput> transformation)
            where TInput : class
            where TOutput : class =>
            pipe.GetRequiredService<IMappingPipeFactory<TInput, TOutput>>()
                .Resolve(transformation, pipe);

        public static OutputPipe<TOutput> UseTransformer<TInput, TOutput>(
            this IOutputPipe<TInput> pipe,
            Func<ITransformerFactory<TInput>, ITransformer<TInput, TOutput>> selection)
            where TInput : class
            where TOutput : class
        {
            var transformer = pipe.GetService<ITransformerFactory<TInput>>();
            return pipe.GetRequiredService<IMappingPipeFactory<TInput, TOutput>>()
                .Resolve(i => selection(transformer).Transform(i), pipe);
        }

        public static OutputPipe<TOutput> BuildTransformation<TInput, TOutput>(
            this IOutputPipe<TInput> pipe,
            Func<ITransformationBuilder<TInput>, Func<TInput, TOutput>> builder)
            where TInput : class
            where TOutput : class
        {
            var transformerBuilder = pipe.GetService<ITransformationBuilder<TInput>>();
            return pipe.GetRequiredService<IMappingPipeFactory<TInput, TOutput>>()
                .Resolve(builder(transformerBuilder), pipe);
        }
    }
}
