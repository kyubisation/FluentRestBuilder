// ReSharper disable once CheckNamespace
namespace KyubiCode.FluentRest
{
    using System;
    using System.Linq;
    using ChainPipes.CollectionTransformation;
    using Microsoft.Extensions.DependencyInjection;
    using Transformers;

    public static partial class Integration
    {
        public static CollectionTransformationPipe<TInput, TOutput> TransformCollection<TInput, TOutput>(
            this IOutputPipe<IQueryable<TInput>> pipe, Func<TInput, TOutput> transformation)
            where TInput : class
            where TOutput : class =>
            pipe.GetRequiredService<ICollectionTransformationPipeFactory<TInput, TOutput>>()
                .Resolve(transformation, pipe);

        public static CollectionTransformationPipe<TInput, TOutput> UseTransformerForCollection<TInput, TOutput>(
            this IOutputPipe<IQueryable<TInput>> pipe,
            Func<ITransformerFactory<TInput>, ITransformer<TInput, TOutput>> selection)
            where TInput : class
            where TOutput : class =>
            pipe.GetRequiredService<ICollectionTransformationPipeFactory<TInput, TOutput>>()
                .ResolveTransformer(selection, pipe);

        public static CollectionTransformationPipe<TInput, TOutput> BuildTransformationForCollection<TInput, TOutput>(
            this IOutputPipe<IQueryable<TInput>> pipe,
            Func<ITransformationBuilder<TInput>, Func<TInput, TOutput>> builder)
            where TInput : class
            where TOutput : class =>
            pipe.GetRequiredService<ICollectionTransformationPipeFactory<TInput, TOutput>>()
                .ResolveTransformationBuilder(builder, pipe);
    }
}
