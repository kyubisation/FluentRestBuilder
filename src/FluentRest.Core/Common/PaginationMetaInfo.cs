// <copyright file="PaginationMetaInfo.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.Common
{
    public class PaginationMetaInfo
    {
        public PaginationMetaInfo(
            int total,
            int page,
            int entriesPerPage,
            int totalPages)
        {
            this.Total = total;
            this.Page = page;
            this.EntriesPerPage = entriesPerPage;
            this.TotalPages = totalPages;
        }

        public int Total { get; }

        public int Page { get; }

        public int EntriesPerPage { get; }

        public int TotalPages { get; }
    }
}
