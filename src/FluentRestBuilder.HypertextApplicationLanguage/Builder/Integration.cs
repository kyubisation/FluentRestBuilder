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
        /// <summary>
        /// Registers the Hypertext Application Language pipes.
        /// </summary>
        /// <param name="builder">The FluentRestBuilder configuration instance.</param>
        /// <returns>The given FluentRestBuilder configuration instance.</returns>
        // ReSharper disable once InconsistentNaming
        public static IFluentRestBuilderConfiguration AddHAL(
            this IFluentRestBuilderConfiguration builder)
        {
            new FluentRestBuilderCoreConfiguration(builder.Services)
                .RegisterCollectionMappingPipe();
            return builder;
        }

        /// <summary>
        /// Registers the <see cref="IActionContextAccessor"/> to be used to create an instance
        /// of <see cref="IUrlHelper"/>.
        /// By default the controller extension methods to create the sources use the
        /// <see cref="IUrlHelper"/> provided by the controller.
        /// This is only necessary if you want to use pipes that require the
        /// <see cref="IUrlHelper"/> and want to create sources without a controller.
        /// </summary>
        /// <param name="builder">The FluentRestBuilder configuration instance.</param>
        /// <returns>The provided FluentRestBuilder configuration instance.</returns>
        public static IFluentRestBuilderConfiguration UseActionContextAccessorForUrlHelper(
            this IFluentRestBuilderConfiguration builder)
        {
            UseActionContextAccessorForUrlHelper(builder.Services);
            return builder;
        }

        /// <summary>
        /// Registers the <see cref="IActionContextAccessor"/> to be used to create an instance
        /// of <see cref="IUrlHelper"/>.
        /// By default the controller extension methods to create the sources use the
        /// <see cref="IUrlHelper"/> provided by the controller.
        /// This is only necessary if you want to use pipes that require the
        /// <see cref="IUrlHelper"/> and want to create sources without a controller.
        /// </summary>
        /// <param name="builder">The FluentRestBuilderCore configuration instance.</param>
        /// <returns>The provided FluentRestBuilderCore configuration instance.</returns>
        public static IFluentRestBuilderCoreConfiguration UseActionContextAccessorForUrlHelper(
            this IFluentRestBuilderCoreConfiguration builder)
        {
            UseActionContextAccessorForUrlHelper(builder.Services);
            return builder;
        }

        /// <summary>
        /// Add a REST mapper to be used in response mapping.
        /// Expects the resulting object to inherit from <see cref="RestEntity"/>.
        /// The resulting object may implement the <see cref="ILinkGenerator{TInput}"/>
        /// to create appropriate reference links.
        ///
        /// Use <see cref="RestMapper{TInput,TOutput}.Create"/> to create an instance
        /// of the <see cref="RestMapper{TInput,TOutput}"/>.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <typeparam name="TOutput">The output type.</typeparam>
        /// <param name="builder">The FluentRestBuilder configuration instance.</param>
        /// <param name="factory">
        /// The factory function to create a <see cref="RestMapper{TInput,TOutput}"/>.
        /// </param>
        /// <returns>The provided FluentRestBuilder configuration instance.</returns>
        public static IFluentRestBuilderConfiguration AddRestMapper<TInput, TOutput>(
            this IFluentRestBuilderConfiguration builder,
            Func<IServiceProvider, RestMapper<TInput, TOutput>> factory)
            where TOutput : RestEntity
        {
            AddRestMapper(builder.Services, factory);
            return builder;
        }

        /// <summary>
        /// Add a REST mapper to be used in response mapping.
        /// Expects the resulting object to inherit from <see cref="RestEntity"/>.
        /// The resulting object may implement the <see cref="ILinkGenerator{TInput}"/>
        /// to create appropriate reference links.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <typeparam name="TOutput">The output type.</typeparam>
        /// <param name="builder">The FluentRestBuilder configuration instance.</param>
        /// <param name="mapping">The mapping function.</param>
        /// <returns>The provided FluentRestBuilder configuration instance.</returns>
        public static IFluentRestBuilderConfiguration AddRestMapper<TInput, TOutput>(
            this IFluentRestBuilderConfiguration builder, Func<TInput, TOutput> mapping)
            where TOutput : RestEntity
        {
            AddRestMapper(builder.Services, mapping);
            return builder;
        }

        /// <summary>
        /// Add a REST mapper to be used in response mapping.
        /// Expects the resulting object to inherit from <see cref="RestEntity"/>.
        /// The resulting object may implement the <see cref="ILinkGenerator{TInput}"/>
        /// to create appropriate reference links.
        ///
        /// Use <see cref="RestMapper{TInput,TOutput}.Create"/> to create an instance
        /// of the <see cref="RestMapper{TInput,TOutput}"/>.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <typeparam name="TOutput">The output type.</typeparam>
        /// <param name="builder">The FluentRestBuilderCore configuration instance.</param>
        /// <param name="factory">
        /// The factory function to create a <see cref="RestMapper{TInput,TOutput}"/>.
        /// </param>
        /// <returns>The provided FluentRestBuilderCore configuration instance.</returns>
        public static IFluentRestBuilderCoreConfiguration AddRestMapper<TInput, TOutput>(
            this IFluentRestBuilderCoreConfiguration builder,
            Func<IServiceProvider, RestMapper<TInput, TOutput>> factory)
            where TOutput : RestEntity
        {
            AddRestMapper(builder.Services, factory);
            return builder;
        }

        /// <summary>
        /// Add a REST mapper to be used in response mapping.
        /// Expects the resulting object to inherit from <see cref="RestEntity"/>.
        /// The resulting object may implement the <see cref="ILinkGenerator{TInput}"/>
        /// to create appropriate reference links.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <typeparam name="TOutput">The output type.</typeparam>
        /// <param name="builder">The FluentRestBuilderCore configuration instance.</param>
        /// <param name="mapping">The mapping function.</param>
        /// <returns>The provided FluentRestBuilderCore configuration instance.</returns>
        public static IFluentRestBuilderCoreConfiguration AddRestMapper<TInput, TOutput>(
            this IFluentRestBuilderCoreConfiguration builder, Func<TInput, TOutput> mapping)
            where TOutput : RestEntity
        {
            AddRestMapper(builder.Services, mapping);
            return builder;
        }

        private static void AddRestMapper<TInput, TOutput>(
            IServiceCollection services, Func<TInput, TOutput> mapping)
            where TOutput : RestEntity
        {
            AddRestMapper(
                services,
                serviceProvider => RestMapper<TInput, TOutput>.Create(serviceProvider, mapping));
        }

        private static void AddRestMapper<TInput, TOutput>(
            IServiceCollection services,
            Func<IServiceProvider, RestMapper<TInput, TOutput>> factory)
            where TOutput : RestEntity
        {
            services.TryAddSingleton<ILinkAggregator, LinkAggregator>();
            services.RegisterMappingServices();
            services.AddScoped<IMapper<TInput, TOutput>>(factory);
        }

        private static void UseActionContextAccessorForUrlHelper(IServiceCollection services)
        {
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IScopedStorage<IUrlHelper>, UrlHelperStorage>();
        }
    }
}
