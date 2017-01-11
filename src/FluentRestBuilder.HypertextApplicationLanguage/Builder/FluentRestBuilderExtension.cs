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
            builder.Services.TryAddScoped<IMapperFactory, MapperFactory>();
            builder.Services.TryAddScoped(typeof(IMapperFactory<>), typeof(MapperFactory<>));
            builder.Services.TryAddTransient(typeof(IMappingBuilder<>), typeof(MappingBuilder<>));
            new FluentRestBuilderCore(builder.Services)
                .RegisterCollectionMappingPipe();
            return builder;
        }

        public static IFluentRestBuilder AddRestMapper<TInput, TOutput>(
            this IFluentRestBuilder builder,
            Func<TInput, TOutput> mapping,
            Action<RestMapper<TInput, TOutput>> configuration = null)
            where TOutput : RestEntity
        {
            AddRestMapper(builder.Services, mapping, configuration);
            return builder;
        }

        public static IFluentRestBuilderCore AddRestMapper<TInput, TOutput>(
            this IFluentRestBuilderCore builder,
            Func<TInput, TOutput> mapping,
            Action<RestMapper<TInput, TOutput>> configuration = null)
            where TOutput : RestEntity
        {
            AddRestMapper(builder.Services, mapping, configuration);
            return builder;
        }

        private static void AddRestMapper<TInput, TOutput>(
            IServiceCollection services,
            Func<TInput, TOutput> mapping,
            Action<RestMapper<TInput, TOutput>> configuration)
            where TOutput : RestEntity
        {
            services.AddScoped<IMapper<TInput, TOutput>>(
                serviceProvider =>
                {
                    var urlHelper = serviceProvider.GetService<IScopedStorage<IUrlHelper>>()?.Value
                        ?? serviceProvider.GetService<IUrlHelper>();
                    var mapper = new RestMapper<TInput, TOutput>(mapping, urlHelper);
                    configuration?.Invoke(mapper);
                    return mapper;
                });
        }
    }
}
