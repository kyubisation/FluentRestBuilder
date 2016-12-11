// <copyright file="IRestCollectionPagination.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.RestCollectionMutators.Pagination
{
    using Common;

    public interface IRestCollectionPagination<TEntity> : IRestCollectionMutator<TEntity>
    {
        int EntriesPerPageDefaultValue { get; set; }

        int MaxEntriesPerPage { get; set; }

        int ActualPage { get; }

        int ActualEntriesPerPage { get; }
    }
}
