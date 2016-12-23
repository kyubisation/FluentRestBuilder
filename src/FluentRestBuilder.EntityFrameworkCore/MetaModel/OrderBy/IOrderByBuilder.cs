// <copyright file="IOrderByBuilder.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.MetaModel.OrderBy
{
    using System;
    using System.Linq.Expressions;
    using FluentRestBuilder.RestCollectionMutators.OrderBy;

    public interface IOrderByBuilder<TEntity>
    {
        OrderByDirection ResolveDirection(string orderBy);

        Expression<Func<TEntity, object>> CreateOrderBy();
    }
}
