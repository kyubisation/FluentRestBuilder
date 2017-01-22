// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Linq;
    using Builder;
    using HypertextApplicationLanguage;
    using HypertextApplicationLanguage.Links;
    using HypertextApplicationLanguage.Mapping;
    using HypertextApplicationLanguage.Pipes.CollectionMapping;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Pipes;

    public static partial class Integration
    {
        public static IFluentRestBuilderCore RegisterCollectionMappingPipe(
            this IFluentRestBuilderCore builder)
        {
            builder.Services.TryAddSingleton<ILinkAggregator, LinkAggregator>();
            builder.Services.RegisterMappingServices();
            builder.Services.TryAddScoped(
                typeof(ICollectionMappingPipeFactory<,>), typeof(CollectionMappingPipeFactory<,>));
            builder.Services
                .TryAddScoped<IRestCollectionLinkGenerator, RestCollectionLinkGenerator>();
            builder.Services.TryAddSingleton(
                typeof(IQueryableTransformer<>), typeof(QueryableTransformer<>));
            return builder;
        }

        public static OutputPipe<RestEntityCollection> MapToRestCollection<TInput, TOutput>(
            this IOutputPipe<IQueryable<TInput>> pipe, Func<TInput, TOutput> mapping)
            where TInput : class
            where TOutput : class =>
            pipe.GetRequiredService<ICollectionMappingPipeFactory<TInput, TOutput>>()
                .Create(mapping, pipe);

        public static OutputPipe<RestEntityCollection> UseMapperForCollection<TInput, TOutput>(
            this IOutputPipe<IQueryable<TInput>> pipe,
            Func<IMapperFactory<TInput>, IMapper<TInput, TOutput>> selection)
            where TInput : class
            where TOutput : class
        {
            var transformer = pipe.GetService<IMapperFactory<TInput>>();
            return pipe.MapToRestCollection(i => selection(transformer).Map(i));
        }

        public static OutputPipe<RestEntityCollection> BuildMappingForCollection<TInput, TOutput>(
            this IOutputPipe<IQueryable<TInput>> pipe,
            Func<IMappingBuilder<TInput>, Func<TInput, TOutput>> builder)
            where TInput : class
            where TOutput : class
        {
            var transformerBuilder = pipe.GetService<IMappingBuilder<TInput>>();
            return pipe.MapToRestCollection(builder(transformerBuilder));
        }
    }
}
