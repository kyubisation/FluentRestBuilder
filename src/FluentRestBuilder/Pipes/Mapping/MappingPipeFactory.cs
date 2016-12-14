// <copyright file="MappingPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.Mapping
{
    using System;
    using Common;
    using Transformers;

    public class MappingPipeFactory<TInput, TOutput> :
        IMappingPipeFactory<TInput, TOutput>
        where TInput : class
        where TOutput : class
    {
        private readonly LazyResolver<ITransformationBuilder<TInput>> transformationBuilder;
        private readonly LazyResolver<ITransformerFactory<TInput>> transformerFactory;

        public MappingPipeFactory(
            LazyResolver<ITransformerFactory<TInput>> transformerFactory,
            LazyResolver<ITransformationBuilder<TInput>> transformationBuilder)
        {
            this.transformerFactory = transformerFactory;
            this.transformationBuilder = transformationBuilder;
        }

        public OutputPipe<TOutput> Resolve(
            Func<TInput, TOutput> transformation,
            IOutputPipe<TInput> parent) =>
            new MappingPipe<TInput, TOutput>(transformation, parent);

        public OutputPipe<TOutput> ResolveTransformer(
            Func<ITransformerFactory<TInput>, ITransformer<TInput, TOutput>> selection,
            IOutputPipe<TInput> parent)
        {
            var transformer = selection(this.transformerFactory.Value);
            return new MappingPipe<TInput, TOutput>(transformer.Transform, parent);
        }

        public OutputPipe<TOutput> ResolveTransformationBuilder(
            Func<ITransformationBuilder<TInput>, Func<TInput, TOutput>> builder,
            IOutputPipe<TInput> parent)
        {
            var transformation = builder(this.transformationBuilder.Value);
            return new MappingPipe<TInput, TOutput>(transformation, parent);
        }
    }
}