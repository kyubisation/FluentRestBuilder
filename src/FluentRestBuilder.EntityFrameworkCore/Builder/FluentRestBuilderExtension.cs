// <copyright file="FluentRestBuilderExtension.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using Builder;
    using EntityFrameworkCore.Builder;
    using Microsoft.EntityFrameworkCore;

    public static class FluentRestBuilderExtension
    {
        public static IFluentRestBuilder AddEntityFrameworkCorePipes<TContext>(
            this IFluentRestBuilder builder)
            where TContext : DbContext
        {
            new FluentRestBuilderCoreEntityFrameworkCore<TContext>(builder.Services)
                .RegisterDeletionPipe()
                .RegisterInsertionPipe()
                .RegisterQueryableSourcePipe()
                .RegisterReloadPipe()
                .RegisterUpdatePipe()
                .RegisterQueryableSource()
                .RegisterEntitySource();
            return builder;
        }
    }
}
