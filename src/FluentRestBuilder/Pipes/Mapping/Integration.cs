// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Pipes.Mapping;
    using Transformers;

    public static partial class Integration
    {
        public static OutputPipe<TOutput> Map<TInput, TOutput>(
            this IOutputPipe<TInput> pipe, Func<TInput, Task<TOutput>> transformation)
            where TInput : class
            where TOutput : class =>
            pipe.GetRequiredService<IMappingPipeFactory<TInput, TOutput>>()
                .Resolve(transformation, pipe);

        public static OutputPipe<TOutput> Map<TInput, TOutput>(
            this IOutputPipe<TInput> pipe, Func<TInput, TOutput> transformation)
            where TInput : class
            where TOutput : class =>
            pipe.Map(i => Task.FromResult(transformation(i)));

        public static OutputPipe<TOutput> UseTransformer<TInput, TOutput>(
            this IOutputPipe<TInput> pipe,
            Func<ITransformerFactory<TInput>, ITransformer<TInput, TOutput>> selection)
            where TInput : class
            where TOutput : class
        {
            var transformer = pipe.GetService<ITransformerFactory<TInput>>();
            return pipe.Map(i => selection(transformer).Transform(i));
        }

        public static OutputPipe<TOutput> BuildTransformation<TInput, TOutput>(
            this IOutputPipe<TInput> pipe,
            Func<ITransformationBuilder<TInput>, Func<TInput, TOutput>> builder)
            where TInput : class
            where TOutput : class
        {
            var transformerBuilder = pipe.GetService<ITransformationBuilder<TInput>>();
            return pipe.Map(builder(transformerBuilder));
        }
    }
}
