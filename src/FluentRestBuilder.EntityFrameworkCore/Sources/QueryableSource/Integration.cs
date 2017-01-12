// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using Builder;
    using EntityFrameworkCore;
    using EntityFrameworkCore.Sources.QueryableSource;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public static partial class Integration
    {
        public static IFluentRestBuilderCore RegisterQueryableSource(
            this IFluentRestBuilderCore builder)
        {
            builder.Services.TryAddSingleton(
                typeof(IQueryableSourceFactory<>), typeof(QueryableSourceFactory<>));
            return builder;
        }

        public static QueryableSource<TEntity> FromQueryable<TEntity>(
            this ControllerBase controller)
            where TEntity : class =>
            controller.HttpContext.RequestServices.GetService<IQueryableSourceFactory<TEntity>>()
                .Resolve(controller);
    }
}
