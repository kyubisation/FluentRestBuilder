// <copyright file="CollectionTransformationPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Pipes.CollectionTransformation
{
    using System;
    using System.Linq;
    using FluentRestBuilder.Common;
    using Hal;
    using Storage;
    using Transformers;

    public class CollectionTransformationPipeFactory<TInput, TOutput> :
        ICollectionTransformationPipeFactory<TInput, TOutput>
    {
        private readonly IRestCollectionLinkGenerator linkGenerator;
        private readonly IScopedStorage<PaginationMetaInfo> paginationMetaInfoStorage;
        private readonly LazyResolver<ITransformationBuilder<TInput>> transformationBuilder;
        private readonly LazyResolver<ITransformerFactory<TInput>> transformerFactory;

        public CollectionTransformationPipeFactory(
            IRestCollectionLinkGenerator linkGenerator,
            IScopedStorage<PaginationMetaInfo> paginationMetaInfoStorage,
            LazyResolver<ITransformerFactory<TInput>> transformerFactory,
            LazyResolver<ITransformationBuilder<TInput>> transformationBuilder)
        {
            this.linkGenerator = linkGenerator;
            this.paginationMetaInfoStorage = paginationMetaInfoStorage;
            this.transformerFactory = transformerFactory;
            this.transformationBuilder = transformationBuilder;
        }

        public OutputPipe<RestEntityCollection> Resolve(
                Func<TInput, TOutput> transformation, IOutputPipe<IQueryable<TInput>> parent) =>
            new CollectionTransformationPipe<TInput, TOutput>(
                transformation, this.linkGenerator, this.paginationMetaInfoStorage, parent);

        public OutputPipe<RestEntityCollection> ResolveTransformer(
            Func<ITransformerFactory<TInput>, ITransformer<TInput, TOutput>> selection,
            IOutputPipe<IQueryable<TInput>> parent)
        {
            var transformer = selection(this.transformerFactory.Value);
            return this.Resolve(transformer.Transform, parent);
        }

        public OutputPipe<RestEntityCollection> ResolveTransformationBuilder(
            Func<ITransformationBuilder<TInput>, Func<TInput, TOutput>> builder,
            IOutputPipe<IQueryable<TInput>> parent)
        {
            var transformation = builder(this.transformationBuilder.Value);
            return this.Resolve(transformation, parent);
        }
    }
}