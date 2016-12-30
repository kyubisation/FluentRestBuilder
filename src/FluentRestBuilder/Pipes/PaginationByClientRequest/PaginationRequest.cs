// <copyright file="PaginationRequest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.PaginationByClientRequest
{
    public class PaginationRequest
    {
        public PaginationRequest(int? page, int? entriesPerPage)
        {
            this.Page = page;
            this.EntriesPerPage = entriesPerPage;
        }

        public int? Page { get; set; }

        public int? EntriesPerPage { get; set; }
    }
}
