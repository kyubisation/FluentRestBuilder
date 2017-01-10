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
        public static IFluentRestBuilder AddFluentRest(this IMvcBuilder builder) =>
            builder.Services.AddFluentRest();

        public static IFluentRestBuilder AddFluentRest(this IMvcCoreBuilder builder) =>
            builder.Services.AddFluentRest();

        public static IFluentRestBuilder AddFluentRest(this IServiceCollection collection) =>
            new FluentRestBuilder(collection);

        public static IFluentRestBuilder RegisterUrlHelper(this IFluentRestBuilder builder)
        {
            if (builder.Services.Any(d => d.ServiceType == typeof(IUrlHelper)))
            {
                return builder;
            }

            builder.Services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
            builder.Services.TryAddScoped(serviceProvider =>
            {
                var actionContextAccessor = serviceProvider.GetService<IActionContextAccessor>();
                var urlHelperFactory = serviceProvider.GetService<IUrlHelperFactory>();
                return urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);
            });

            return builder;
        }
    }
}
