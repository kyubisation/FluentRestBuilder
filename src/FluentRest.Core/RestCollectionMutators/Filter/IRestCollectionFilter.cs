// <copyright file="IRestCollectionFilter.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.RestCollectionMutators.Filter
{
    using Common;

    public interface IRestCollectionFilter<TEntity> : IRestCollectionMutator<TEntity>
    {
    }
}
