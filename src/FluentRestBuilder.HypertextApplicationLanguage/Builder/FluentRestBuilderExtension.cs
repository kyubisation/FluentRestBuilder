// <copyright file="FluentRestBuilderExtension.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using Builder;
    using HypertextApplicationLanguage;
    using HypertextApplicationLanguage.Mapping;
    using HypertextApplicationLanguage.Pipes.CollectionMapping;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Storage;

    public static class FluentRestBuilderExtension
    {
        // ReSharper disable once InconsistentNaming
        public static IFluentRestBuilder AddHAL(
            this IFluentRestBuilder builder)
        {
            RegisterMappings(builder);
            builder.Services.TryAddScoped(
                typeof(ICollectionMappingPipeFactory<,>), typeof(CollectionMappingPipeFactory<,>));
            builder.Services
                .TryAddScoped<IRestCollectionLinkGenerator, RestCollectionLinkGenerator>();
            return builder;
        }

        public static IFluentRestBuilder AddRestMapper<TInput, TOutput>(
            this IFluentRestBuilder builder,
            Func<TInput, TOutput> mapping,
            Action<RestMapper<TInput, TOutput>> configuration = null)
            where TOutput : RestEntity
        {
            builder.Services.AddScoped<IMapper<TInput, TOutput>>(
                serviceProvider =>
                {
                    var urlHelper = serviceProvider.GetService<IScopedStorage<IUrlHelper>>()?.Value
                        ?? serviceProvider.GetService<IUrlHelper>();
                    var mapper = new RestMapper<TInput, TOutput>(mapping, urlHelper);
                    configuration?.Invoke(mapper);
                    return mapper;
                });
            return builder;
        }

        private static void RegisterMappings(IFluentRestBuilder builder)
        {
            builder.Services.TryAddScoped<IMapperFactory, MapperFactory>();
            builder.Services.TryAddScoped(typeof(IMapperFactory<>), typeof(MapperFactory<>));
            builder.Services.TryAddTransient(typeof(IMappingBuilder<>), typeof(MappingBuilder<>));
        }
    }
}
