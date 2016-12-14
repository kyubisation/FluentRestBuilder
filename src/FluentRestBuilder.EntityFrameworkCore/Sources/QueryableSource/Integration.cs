// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using EntityFrameworkCore.Sources.QueryableSource;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;

    public static partial class Integration
    {
        public static QueryableSource<TEntity> FromQueryable<TEntity>(this ControllerBase controller)
            where TEntity : class =>
            controller.HttpContext.RequestServices
                .GetService<IQueryableSourceFactory<TEntity>>()
                .Resolve();
    }
}
