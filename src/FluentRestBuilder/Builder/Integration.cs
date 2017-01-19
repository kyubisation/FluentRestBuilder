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

        public static IFluentRestBuilder AddFluentRestBuilder(this IMvcBuilder builder) =>
            builder.Services.AddFluentRestBuilder();

        public static IFluentRestBuilder AddFluentRestBuilder(this IMvcCoreBuilder builder) =>
            builder.Services.AddFluentRestBuilder();

        public static IFluentRestBuilder AddFluentRestBuilder(this IServiceCollection collection) =>
            new FluentRestBuilder(collection);

        public static IFluentRestBuilderCore UseHttpContextAccessor(this IFluentRestBuilderCore builder)
        {
            UseHttpContextAccessor(builder.Services);
            return builder;
        }

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
