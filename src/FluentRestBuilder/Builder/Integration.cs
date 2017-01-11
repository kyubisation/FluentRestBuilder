// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System.Linq;
    using Builder;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public static partial class Integration
    {
        public static IFluentRestBuilderCore AddFluentRestBuilderCore(this IMvcBuilder builder) =>
            builder.Services.AddFluentRestBuilderCore();

        public static IFluentRestBuilderCore AddFluentRestBuilderCore(this IMvcCoreBuilder builder) =>
            builder.Services.AddFluentRestBuilderCore();

        public static IFluentRestBuilderCore AddFluentRestBuilderCore(this IServiceCollection collection) =>
            new FluentRestBuilderCore(collection).RegisterStorage();

        public static IFluentRestBuilder AddFluentRestBuilder(this IMvcBuilder builder) =>
            builder.Services.AddFluentRestBuilder();

        public static IFluentRestBuilder AddFluentRestBuilder(this IMvcCoreBuilder builder) =>
            builder.Services.AddFluentRestBuilder();

        public static IFluentRestBuilder AddFluentRestBuilder(this IServiceCollection collection) =>
            new FluentRestBuilder(collection);

        public static IFluentRestBuilderCore RegisterUrlHelper(this IFluentRestBuilderCore builder)
        {
            RegisterUrlHelper(builder.Services);
            return builder;
        }

        public static IFluentRestBuilder RegisterUrlHelper(this IFluentRestBuilder builder)
        {
            RegisterUrlHelper(builder.Services);
            return builder;
        }

        private static void RegisterUrlHelper(IServiceCollection services)
        {
            if (services.Any(d => d.ServiceType == typeof(IUrlHelper)))
            {
                return;
            }

            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.TryAddScoped(serviceProvider =>
            {
                var actionContextAccessor = serviceProvider.GetService<IActionContextAccessor>();
                var urlHelperFactory = serviceProvider.GetService<IUrlHelperFactory>();
                return urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);
            });
        }
    }
}
