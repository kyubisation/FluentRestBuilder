// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using Builder;
    using HypertextApplicationLanguage;
    using HypertextApplicationLanguage.Links;
    using HypertextApplicationLanguage.Mapping;
    using HypertextApplicationLanguage.Storage;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Storage;

    public static partial class Integration
    {
        // ReSharper disable once InconsistentNaming
        public static IFluentRestBuilder AddHAL(
            this IFluentRestBuilder builder)
        {
            new FluentRestBuilderCore(builder.Services)
                .RegisterCollectionMappingPipe();
            return builder;
        }

        public static IFluentRestBuilder UseActionContextAccessorForUrlHelper(
            this IFluentRestBuilder builder)
        {
            UseActionContextAccessorForUrlHelper(builder.Services);
            return builder;
        }

        public static IFluentRestBuilderCore UseActionContextAccessorForUrlHelper(
            this IFluentRestBuilderCore builder)
        {
            UseActionContextAccessorForUrlHelper(builder.Services);
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
            services.TryAddSingleton<ILinkAggregator, LinkAggregator>();
            services.RegisterMappingServices();
            services.AddScoped<IMapper<TInput, TOutput>>(
                serviceProvider =>
                {
                    var urlHelper = serviceProvider.GetService<IScopedStorage<IUrlHelper>>();
                    var linkAggregator = serviceProvider.GetService<ILinkAggregator>();
                    var mapper = new RestMapper<TInput, TOutput>(
                        mapping, urlHelper, linkAggregator);
                    configuration?.Invoke(mapper);
                    return mapper;
                });
        }

        private static void UseActionContextAccessorForUrlHelper(IServiceCollection services)
        {
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IScopedStorage<IUrlHelper>, UrlHelperStorage>();
        }
    }
}
