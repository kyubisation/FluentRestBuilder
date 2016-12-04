// <copyright file="ITransformationPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.Pipes.Transformation
{
    using System;
    using Transformers;

    public interface ITransformationPipeFactory<TInput, TOutput>
        where TInput : class
        where TOutput : class
    {
        TransformationPipe<TInput, TOutput> Resolve(
            Func<TInput, TOutput> transformation, IOutputPipe<TInput> parent);

        TransformationPipe<TInput, TOutput> ResolveTransformer(
            Func<ITransformerFactory<TInput>, ITransformer<TInput, TOutput>> selection,
            IOutputPipe<TInput> parent);

        TransformationPipe<TInput, TOutput> ResolveTransformationBuilder(
            Func<ITransformationBuilder<TInput>, Func<TInput, TOutput>> builder,
            IOutputPipe<TInput> parent);
    }
}
