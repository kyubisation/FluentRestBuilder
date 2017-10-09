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

    public static class Integration
    {
        /// <summary>
        /// Registers the <see cref="IActionContextAccessor"/> to be used to create an instance
        /// of <see cref="IUrlHelper"/>.
        /// By default the controller extension methods to create the observables use the
        /// <see cref="IUrlHelper"/> provided by the controller.
        /// This is only necessary if you want to use operators that require the
        /// <see cref="IUrlHelper"/> and want to create observables without a controller.
        /// </summary>
        /// <param name="builder">The FluentRestBuilder configuration instance.</param>
        /// <returns>The provided FluentRestBuilder configuration instance.</returns>
        public static IFluentRestBuilderConfiguration UseActionContextAccessorForUrlHelper(
            this IFluentRestBuilderConfiguration builder)
        {
            builder.Services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
            builder.Services.AddScoped<IScopedStorage<IUrlHelper>, UrlHelperStorage>();
            return builder;
        }
    }
}
