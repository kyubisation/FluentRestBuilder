// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRest
{
    using EntityFrameworkCore.Sources.EntityCollection;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;

    public static partial class Integration
    {
        public static EntityCollectionSource<TEntity> SelectEntityCollection<TEntity>(
            this ControllerBase controller)
            where TEntity : class =>
            controller.HttpContext.RequestServices
                .GetService<IEntityCollectionSourceFactory<TEntity>>()
                .Resolve();
    }
}
