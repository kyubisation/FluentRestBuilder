// <copyright file="StartupIntegrationExtensions.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core
{
    using Microsoft.Extensions.DependencyInjection;

    public static class StartupIntegrationExtensions
    {
        public static IFluentRestBuilder AddFluentRest(this IMvcBuilder builder) =>
            builder.Services.AddFluentRest();

        public static IFluentRestBuilder AddFluentRest(this IMvcCoreBuilder builder) =>
            builder.Services.AddFluentRest();

        public static IFluentRestBuilder AddFluentRest(this IServiceCollection collection) =>
            new FluentRestBuilder(collection);
    }
}
