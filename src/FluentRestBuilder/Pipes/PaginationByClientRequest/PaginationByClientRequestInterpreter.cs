// <copyright file="PaginationByClientRequestInterpreter.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.PaginationByClientRequest
{
    using Exceptions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Primitives;
    using Storage;

    public class PaginationByClientRequestInterpreter : IPaginationByClientRequestInterpreter
    {
        private readonly IQueryArgumentKeys queryArgumentKeys;
        private readonly IQueryCollection queryCollection;

        public PaginationByClientRequestInterpreter(
            IScopedStorage<HttpContext> httpContextStorage,
            IQueryArgumentKeys queryArgumentKeys)
        {
            this.queryCollection = httpContextStorage.Value.Request.Query;
            this.queryArgumentKeys = queryArgumentKeys;
        }

        public PaginationRequest ParseRequestQuery() =>
            new PaginationRequest(this.ParsePage(), this.ParseEntriesPerPage());

        private int? ParsePage()
        {
            StringValues pageValue;
            if (!this.queryCollection.TryGetValue(this.queryArgumentKeys.Page, out pageValue)
                || string.IsNullOrEmpty(pageValue.ToString()))
            {
                return null;
            }

            int page;
            if (!int.TryParse(pageValue.ToString(), out page) || page < 1)
            {
                throw new NotSupportedPageException(pageValue.ToString());
            }

            return page;
        }

        private int? ParseEntriesPerPage()
        {
            StringValues entriesPerPageValue;
            if (!this.queryCollection
                .TryGetValue(this.queryArgumentKeys.EntriesPerPage, out entriesPerPageValue)
                || string.IsNullOrEmpty(entriesPerPageValue.ToString()))
            {
                return null;
            }

            int entriesPerPage;
            if (!int.TryParse(entriesPerPageValue.ToString(), out entriesPerPage)
                || entriesPerPage < 1)
            {
                throw new NotSupportedEntriesPerPageException(entriesPerPageValue.ToString());
            }

            return entriesPerPage;
        }
    }
}