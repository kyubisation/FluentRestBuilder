// <copyright file="IPrimaryKeyExpressionFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.MetaModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IPrimaryKeyExpressionFactory<TEntity>
    {
        Expression<Func<TEntity, bool>> CreatePrimaryKeyFilterExpression(IReadOnlyCollection<object> keys);
    }
}
