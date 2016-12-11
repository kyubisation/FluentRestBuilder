// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using Builder;
    using Microsoft.Extensions.DependencyInjection;

    public static partial class Integration
    {
        public static IFluentRestBuilder AddFluentRest(this IMvcBuilder builder) =>
            builder.Services.AddFluentRest();

        public static IFluentRestBuilder AddFluentRest(this IMvcCoreBuilder builder) =>
            builder.Services.AddFluentRest();

        public static IFluentRestBuilder AddFluentRest(this IServiceCollection collection) =>
            new FluentRestBuilder(collection);
    }
}
