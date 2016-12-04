// <copyright file="IRestCollectionMutator.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.RestCollectionMutators.Common
{
    using System.Linq;

    public interface IRestCollectionMutator<TEntity>
    {
        IQueryable<TEntity> Apply(IQueryable<TEntity> queryable);
    }
}