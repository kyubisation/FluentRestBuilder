// <copyright file="FluentRestBuilderCoreExtension.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using Builder;
    using EntityFrameworkCore.Builder;
    using Microsoft.EntityFrameworkCore;

    public static class FluentRestBuilderCoreExtension
    {
        public static IFluentRestBuilderCoreEntityFrameworkCore RegisterContext<TContext>(
            this IFluentRestBuilderCore builder)
            where TContext : DbContext =>
            new FluentRestBuilderCoreEntityFrameworkCore<TContext>(builder.Services);
    }
}
