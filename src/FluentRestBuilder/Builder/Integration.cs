// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Linq;
    using System.Reflection;
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

        public static IServiceCollection TryAddPipeWithScopedNestedFactory(
            this IServiceCollection services, Type pipe)
        {
            TryAddPipeWithNestedFactory(services.TryAddScoped, pipe);
            return services;
        }

        public static IServiceCollection TryAddPipeWithSingletonNestedFactory(
            this IServiceCollection services, Type pipe)
        {
            TryAddPipeWithNestedFactory(services.TryAddSingleton, pipe);
            return services;
        }

        private static void TryAddPipeWithNestedFactory(
            Action<Type, Type> registration, Type pipe)
        {
            var factories = pipe.GetTypeInfo()
                .GetNestedTypes()
                .Where(t => t.Name.Contains("Factory"));
            foreach (var factory in factories)
            {
                var factoryInterfaces = factory.GetInterfaces()
                    .Select(i => i.GetGenericTypeDefinition());
                foreach (var factoryInterface in factoryInterfaces)
                {
                    registration(factoryInterface, factory);
                }
            }
        }
    }
}
