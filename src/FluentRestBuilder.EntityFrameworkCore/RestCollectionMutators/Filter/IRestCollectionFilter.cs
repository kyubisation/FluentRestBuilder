// <copyright file="IRestCollectionFilter.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.RestCollectionMutators.Filter
{
    using Common;

    public interface IRestCollectionFilter<TEntity> : IRestCollectionMutator<TEntity>
    {
    }
}
