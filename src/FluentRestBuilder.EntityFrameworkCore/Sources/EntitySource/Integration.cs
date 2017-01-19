// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Linq.Expressions;
    using EntityFrameworkCore.Builder;
    using EntityFrameworkCore.MetaModel;
    using EntityFrameworkCore.Sources.EntitySource;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public static partial class Integration
    {
        public static IFluentRestBuilderCoreEntityFrameworkCore RegisterEntitySource(
            this IFluentRestBuilderCoreEntityFrameworkCore builder)
        {
            builder.Services.TryAddScoped(
                typeof(IEntitySourceFactory<>), typeof(EntitySourceFactory<>));
            return builder;
        }

        public static OutputPipe<TEntity> WithEntity<TEntity>(
            this ControllerBase controller, Expression<Func<TEntity, bool>> predicate)
            where TEntity : class =>
            controller.HttpContext.RequestServices
                .GetService<IEntitySourceFactory<TEntity>>()
                .Create(controller, predicate);

        public static OutputPipe<TEntity> WithEntity<TEntity>(
            this ControllerBase controller, params object[] primaryKey)
            where TEntity : class
        {
            var factory = controller.HttpContext.RequestServices
                .GetService<IPrimaryKeyExpressionFactory<TEntity>>();
            return controller.WithEntity(factory.CreatePrimaryKeyFilterExpression(primaryKey));
        }
    }
}
