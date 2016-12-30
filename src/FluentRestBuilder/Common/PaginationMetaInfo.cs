// <copyright file="PaginationMetaInfo.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Common
{
    using System;

    public class PaginationMetaInfo
    {
        public PaginationMetaInfo(
            int total,
            int page,
            int entriesPerPage)
        {
            this.Total = total;
            this.Page = page;
            this.EntriesPerPage = entriesPerPage;
            this.TotalPages = (int)Math.Ceiling(total / (double)entriesPerPage);
        }

        public int Total { get; }

        public int Page { get; }

        public int EntriesPerPage { get; }

        public int TotalPages { get; }
    }
}
