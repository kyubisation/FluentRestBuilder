// <copyright file="IRestCollectionMutator.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.RestCollectionMutators.Common
{
    using System.Linq;

    public interface IRestCollectionMutator<TEntity>
    {
        IQueryable<TEntity> Apply(IQueryable<TEntity> queryable);
    }
}