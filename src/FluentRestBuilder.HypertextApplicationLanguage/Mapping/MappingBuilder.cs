// <copyright file="MappingBuilder.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class MappingBuilder<TInput> : IMappingBuilder<TInput>
    {
        private readonly IDictionary<string, Func<TInput, object>> embeddedResourceFactories =
            new Dictionary<string, Func<TInput, object>>();

        private readonly IMapperFactory<TInput> inputMapperFactory;
        private readonly IMapperFactory mapperFactory;

        public MappingBuilder(
            IMapperFactory mapperFactory,
            IMapperFactory<TInput> inputMapperFactory)
        {
            this.mapperFactory = mapperFactory;
            this.inputMapperFactory = inputMapperFactory;
        }

        public IMappingBuilder<TInput> Embed(
            string name, Func<TInput, object> embeddedObjectFactory) =>
            this.AddResourceFactory(name, embeddedObjectFactory);

        public IMappingBuilder<TInput> Embed<TEmbeddedResource, TMappedResource>(
            string name,
            Func<TInput, TEmbeddedResource> resourceSelector,
            Func<
                IMapperFactory<TEmbeddedResource>,
                IMapper<TEmbeddedResource, TMappedResource>> mapperSelector) =>
            this.AddResourceFactory(
                name,
                source => this.ResolveResourceAndApplyMapper(
                    source, resourceSelector, mapperSelector));

        public Func<TInput, TOutput> UseMapper<TOutput>(
            Func<IMapperFactory<TInput>, IMapper<TInput, TOutput>> selection)
        {
            return source =>
            {
                var mapper = this.embeddedResourceFactories.Aggregate(
                    selection(this.inputMapperFactory),
                    (current, next) => current.Embed(next.Key, next.Value(source)));
                return mapper.Map(source);
            };
        }

        private IMappingBuilder<TInput> AddResourceFactory(string name, Func<TInput, object> factory)
        {
            this.embeddedResourceFactories.Add(name, factory);
            return this;
        }

        private TMappedResource ResolveResourceAndApplyMapper<TEmbeddedResource, TMappedResource>(
            TInput source,
            Func<TInput, TEmbeddedResource> resourceSelector,
            Func<
                IMapperFactory<TEmbeddedResource>,
                IMapper<TEmbeddedResource, TMappedResource>> mapperSelector)
        {
            var resource = resourceSelector(source);
            var factory = this.mapperFactory.Resolve<TEmbeddedResource>();
            return mapperSelector(factory).Map(resource);
        }
    }
}
