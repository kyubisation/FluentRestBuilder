namespace KyubiCode.FluentRest.ChainPipes.CollectionTransformation
{
    using System;
    using System.Linq;
    using FluentRest.Common;
    using Transformers;

    public class CollectionTransformationPipeFactory<TInput, TOutput> :
        ICollectionTransformationPipeFactory<TInput, TOutput>
    {
        private readonly IRestCollectionLinkGenerator linkGenerator;
        private readonly LazyResolver<ITransformerFactory<TInput>> transformerFactory;
        private readonly LazyResolver<ITransformationBuilder<TInput>> transformationBuilder;

        public CollectionTransformationPipeFactory(
            IRestCollectionLinkGenerator linkGenerator,
            LazyResolver<ITransformerFactory<TInput>> transformerFactory,
            LazyResolver<ITransformationBuilder<TInput>> transformationBuilder)
        {
            this.linkGenerator = linkGenerator;
            this.transformerFactory = transformerFactory;
            this.transformationBuilder = transformationBuilder;
        }

        public CollectionTransformationPipe<TInput, TOutput> Resolve(
                Func<TInput, TOutput> transformation, IOutputPipe<IQueryable<TInput>> parent) =>
            new CollectionTransformationPipe<TInput, TOutput>(
                transformation, this.linkGenerator, parent);

        public CollectionTransformationPipe<TInput, TOutput> ResolveTransformer(
            Func<ITransformerFactory<TInput>, ITransformer<TInput, TOutput>> selection,
            IOutputPipe<IQueryable<TInput>> parent)
        {
            var transformer = selection(this.transformerFactory.Value);
            return this.Resolve(transformer.Transform, parent);
        }

        public CollectionTransformationPipe<TInput, TOutput> ResolveTransformationBuilder(
            Func<ITransformationBuilder<TInput>, Func<TInput, TOutput>> builder,
            IOutputPipe<IQueryable<TInput>> parent)
        {
            var transformation = builder(this.transformationBuilder.Value);
            return this.Resolve(transformation, parent);
        }
    }
}