// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRest
{
    using System;
    using Core;
    using Core.ChainPipes.Transformation;
    using Core.Transformers;
    using Microsoft.Extensions.DependencyInjection;

    public static partial class Integration
    {
        public static TransformationPipe<TInput, TOutput> Select<TInput, TOutput>(
            this IOutputPipe<TInput> pipe, Func<TInput, TOutput> transformation)
            where TInput : class
            where TOutput : class =>
            pipe.GetRequiredService<ITransformationPipeFactory<TInput, TOutput>>()
                .Resolve(transformation, pipe);

        public static TransformationPipe<TInput, TOutput> UseTransformer<TInput, TOutput>(
            this IOutputPipe<TInput> pipe,
            Func<ITransformerFactory<TInput>, ITransformer<TInput, TOutput>> selection)
            where TInput : class
            where TOutput : class =>
            pipe.GetRequiredService<ITransformationPipeFactory<TInput, TOutput>>()
                .ResolveTransformer(selection, pipe);

        public static TransformationPipe<TInput, TOutput> BuildTransformation<TInput, TOutput>(
            this IOutputPipe<TInput> pipe,
            Func<ITransformationBuilder<TInput>, Func<TInput, TOutput>> builder)
            where TInput : class
            where TOutput : class =>
            pipe.GetRequiredService<ITransformationPipeFactory<TInput, TOutput>>()
                .ResolveTransformationBuilder(builder, pipe);
    }
}
