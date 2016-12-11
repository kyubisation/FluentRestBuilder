// <copyright file="TransformationPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.Pipes.Transformation
{
    using System;
    using Core.Common;
    using Transformers;

    public class TransformationPipeFactory<TInput, TOutput> :
        ITransformationPipeFactory<TInput, TOutput>
        where TInput : class
        where TOutput : class
    {
        private readonly LazyResolver<ITransformationBuilder<TInput>> transformationBuilder;
        private readonly LazyResolver<ITransformerFactory<TInput>> transformerFactory;

        public TransformationPipeFactory(
            LazyResolver<ITransformerFactory<TInput>> transformerFactory,
            LazyResolver<ITransformationBuilder<TInput>> transformationBuilder)
        {
            this.transformerFactory = transformerFactory;
            this.transformationBuilder = transformationBuilder;
        }

        public TransformationPipe<TInput, TOutput> Resolve(
            Func<TInput, TOutput> transformation,
            IOutputPipe<TInput> parent) =>
            new TransformationPipe<TInput, TOutput>(transformation, parent);

        public TransformationPipe<TInput, TOutput> ResolveTransformer(
            Func<ITransformerFactory<TInput>, ITransformer<TInput, TOutput>> selection,
            IOutputPipe<TInput> parent)
        {
            var transformer = selection(this.transformerFactory.Value);
            return new TransformationPipe<TInput, TOutput>(transformer.Transform, parent);
        }

        public TransformationPipe<TInput, TOutput> ResolveTransformationBuilder(
            Func<ITransformationBuilder<TInput>, Func<TInput, TOutput>> builder,
            IOutputPipe<TInput> parent)
        {
            var transformation = builder(this.transformationBuilder.Value);
            return new TransformationPipe<TInput, TOutput>(transformation, parent);
        }
    }
}