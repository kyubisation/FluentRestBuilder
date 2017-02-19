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
        /// <summary>
        /// Registers FluentRestBuilder core.
        /// Does not register any pipe.
        /// </summary>
        /// <param name="builder">The MVC builder.</param>
        /// <returns>The FluentRestBuilderCore configuration instance.</returns>
        public static IFluentRestBuilderCore AddFluentRestBuilderCore(this IMvcBuilder builder) =>
            builder.Services.AddFluentRestBuilderCore();

        /// <summary>
        /// Registers FluentRestBuilder core.
        /// Does not register any pipe.
        /// </summary>
        /// <param name="builder">The MVC core builder.</param>
        /// <returns>The FluentRestBuilderCore configuration instance.</returns>
        public static IFluentRestBuilderCore AddFluentRestBuilderCore(
            this IMvcCoreBuilder builder) =>
            builder.Services.AddFluentRestBuilderCore();

        /// <summary>
        /// Registers FluentRestBuilder core.
        /// Does not register any pipe.
        /// </summary>
        /// <param name="collection">The service collection.</param>
        /// <returns>The FluentRestBuilderCore configuration instance.</returns>
        public static IFluentRestBuilderCore AddFluentRestBuilderCore(
            this IServiceCollection collection) =>
            new FluentRestBuilderCore(collection);

        /// <summary>
        /// Registers FluentRestBuilder.
        /// </summary>
        /// <param name="builder">The MVC builder.</param>
        /// <returns>The FluentRestBuilder configuration instance.</returns>
        public static IFluentRestBuilder AddFluentRestBuilder(this IMvcBuilder builder) =>
            builder.Services.AddFluentRestBuilder();

        /// <summary>
        /// Registers FluentRestBuilder.
        /// </summary>
        /// <param name="builder">The MVC core builder.</param>
        /// <returns>The FluentRestBuilder configuration instance.</returns>
        public static IFluentRestBuilder AddFluentRestBuilder(this IMvcCoreBuilder builder) =>
            builder.Services.AddFluentRestBuilder();

        /// <summary>
        /// Registers FluentRestBuilder.
        /// </summary>
        /// <param name="collection">The service collection.</param>
        /// <returns>The FluentRestBuilder configuration instance.</returns>
        public static IFluentRestBuilder AddFluentRestBuilder(this IServiceCollection collection) =>
            new FluentRestBuilder(collection);

        /// <summary>
        /// Registers the <see cref="IHttpContextAccessor"/> to be used as the access for the
        /// <see cref="HttpContext"/>.
        /// By default the controller extension methods to create the sources use the
        /// <see cref="HttpContext"/> provided by the controller.
        /// This is only necessary if you want to use pipes that require the
        ///  <see cref="HttpContext"/> and want to create sources without a controller.
        /// </summary>
        /// <remarks>
        /// The ASP.NET Core Identity package registers <see cref="IHttpContextAccessor"/>.
        /// </remarks>
        /// <param name="builder">The FluentRestBuilderCore configuration instance.</param>
        /// <returns>The provided FluentRestBuilderCore configuration instance.</returns>
        public static IFluentRestBuilderCore UseHttpContextAccessor(
            this IFluentRestBuilderCore builder)
        {
            UseHttpContextAccessor(builder.Services);
            return builder;
        }

        /// <summary>
        /// Registers the <see cref="IHttpContextAccessor"/> to be used as the access for the
        /// <see cref="HttpContext"/>.
        /// By default the controller extension methods to create the sources use the
        /// <see cref="HttpContext"/> provided by the controller.
        /// This is only necessary if you want to use pipes that require the
        ///  <see cref="HttpContext"/> and want to create sources without a controller.
        /// </summary>
        /// <remarks>
        /// The ASP.NET Core Identity package registers <see cref="IHttpContextAccessor"/>.
        /// </remarks>
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
