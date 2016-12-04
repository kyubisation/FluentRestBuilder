// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRest
{
    using EntityFrameworkCore.Sources.RequestEntity;
    using Microsoft.AspNetCore.Mvc;

    public static partial class Integration
    {
        public static RequestEntitySource<TEntity> From<TEntity>(
            this ControllerBase controller, TEntity entity)
            where TEntity : class =>
            new RequestEntitySource<TEntity>(entity, controller.HttpContext.RequestServices);
    }
}
