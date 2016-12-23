// <copyright file="IRestCollectionPagination.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.RestCollectionMutators.Pagination
{
    public interface IRestCollectionPagination<TEntity> : IRestCollectionMutator<TEntity>
    {
        PaginationOptions Options { get; set; }
    }
}
