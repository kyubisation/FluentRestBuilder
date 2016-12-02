namespace KyubiCode.FluentRest.Transformers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TransformationBuilder<TInput> : ITransformationBuilder<TInput>
    {
        private readonly ITransformerFactory transformerFactory;
        private readonly ITransformerFactory<TInput> inputTransformerFactory;
        private readonly IDictionary<string, Func<TInput, object>> embeddedResourceBuilders =
            new Dictionary<string, Func<TInput, object>>();

        public TransformationBuilder(
            ITransformerFactory transformerFactory,
            ITransformerFactory<TInput> inputTransformerFactory)
        {
            this.transformerFactory = transformerFactory;
            this.inputTransformerFactory = inputTransformerFactory;
        }

        public ITransformationBuilder<TInput> Embed<TEmbeddedResource, TTransformedResource>(
            string name,
            Func<TInput, TEmbeddedResource> resourceSelector,
            Func<
                ITransformerFactory<TEmbeddedResource>,
                ITransformer<TEmbeddedResource, TTransformedResource>>
                transformerSelector)
        {
            this.embeddedResourceBuilders.Add(
                name,
                source =>
            {
                var resource = resourceSelector(source);
                var factory = this.transformerFactory.Resolve<TEmbeddedResource>();
                var transformer = transformerSelector(factory);
                return transformer.Transform(resource);
            });
            return this;
        }

        public Func<TInput, TOutput> UseTransformer<TOutput>(
            Func<ITransformerFactory<TInput>, ITransformer<TInput, TOutput>> selection)
        {
            return source =>
            {
                var transformer = selection(this.inputTransformerFactory);
                this.embeddedResourceBuilders.Aggregate(
                    transformer, (current, next) => current.Embed(next.Key, next.Value(source)));

                return transformer.Transform(source);
            };
        }
    }
}
