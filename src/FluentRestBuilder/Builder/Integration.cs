// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Storage;

    public static partial class Integration
    {
        public static IFluentRestBuilderCore AddFluentRestBuilderCore(this IMvcBuilder builder) =>
            builder.Services.AddFluentRestBuilderCore();

        public static IFluentRestBuilderCore AddFluentRestBuilderCore(this IMvcCoreBuilder builder) =>
            builder.Services.AddFluentRestBuilderCore();

        public static IFluentRestBuilderCore AddFluentRestBuilderCore(this IServiceCollection collection) =>
            new FluentRestBuilderCore(collection);

        /// <summary>
        /// Registers the FluentRestBuilder.
        /// </summary>
        /// <param name="builder">The MVC builder.</param>
        /// <returns>The FluentRestBuilder configuration instance.</returns>
        public static IFluentRestBuilder AddFluentRestBuilder(this IMvcBuilder builder) =>
            builder.Services.AddFluentRestBuilder();

        /// <summary>
        /// Registers the FluentRestBuilder.
        /// </summary>
        /// <param name="builder">The MVC core builder.</param>
        /// <returns>The FluentRestBuilder configuration instance.</returns>
        public static IFluentRestBuilder AddFluentRestBuilder(this IMvcCoreBuilder builder) =>
            builder.Services.AddFluentRestBuilder();

        /// <summary>
        /// Registers the FluentRestBuilder.
        /// </summary>
        /// <param name="collection">The service collection.</param>
        /// <returns>The FluentRestBuilder configuration instance.</returns>
        public static IFluentRestBuilder AddFluentRestBuilder(this IServiceCollection collection) =>
            new FluentRestBuilder(collection);

        public static IFluentRestBuilderCore UseHttpContextAccessor(this IFluentRestBuilderCore builder)
        {
            UseHttpContextAccessor(builder.Services);
            return builder;
        }

        /// <summary>
        /// Registers the IHttpContextAccessor to be used as the access for the HttpContext.
        /// By default the controller extension methods to create the sources use the HttpContext
        /// provided by the controller.
        /// This is only necessary if you want to use pipes that require the HttpContext and
        /// want to create sources without a controller.
        /// </summary>
        /// <param name="builder">The FluentRestBuilder configuration instance.</param>
        /// <returns>The provided FluentRestBuilder configuration instance.</returns>
        public static IFluentRestBuilder UseHttpContextAccessor(this IFluentRestBuilder builder)
        {
            UseHttpContextAccessor(builder.Services);
            return builder;
        }

        private static void UseHttpContextAccessor(IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IScopedStorage<HttpContext>, HttpContextStorage>();
        }
    }
}
