// <copyright file="QueryArgumentKeys.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.EntityFrameworkCore.RestCollectionMutators
{
    public class QueryArgumentKeys : IQueryArgumentKeys
    {
        public string Page { get; set; } = "page";

        public string EntriesPerPage { get; set; } = "entriesPerPage";

        public string Filter { get; set; } = "filter";

        public string OrderBy { get; set; } = "orderBy";

        public string Search { get; set; } = "search";
    }
}