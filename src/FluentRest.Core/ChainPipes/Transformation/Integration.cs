// ReSharper disable once CheckNamespace
namespace KyubiCode.FluentRest
{
    using System;
    using ChainPipes.Transformation;
    using Microsoft.Extensions.DependencyInjection;
    using Transformers;

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
