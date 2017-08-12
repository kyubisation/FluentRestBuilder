// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using Builder;
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

        private static void UseActionContextAccessorForUrlHelper(IServiceCollection services)
        {
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IScopedStorage<IUrlHelper>, UrlHelperStorage>();
        }
    }
}
