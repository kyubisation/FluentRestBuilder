// <copyright file="IMappingPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.Mapping
{
    using System;
    using Transformers;

    public interface IMappingPipeFactory<TInput, TOutput>
        where TInput : class
        where TOutput : class
    {
        OutputPipe<TOutput> Resolve(
            Func<TInput, TOutput> transformation, IOutputPipe<TInput> parent);

        OutputPipe<TOutput> ResolveTransformer(
            Func<ITransformerFactory<TInput>, ITransformer<TInput, TOutput>> selection,
            IOutputPipe<TInput> parent);

        OutputPipe<TOutput> ResolveTransformationBuilder(
            Func<ITransformationBuilder<TInput>, Func<TInput, TOutput>> builder,
            IOutputPipe<TInput> parent);
    }
}
