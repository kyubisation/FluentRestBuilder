// <copyright file="HttpContextStorage.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Common.Mocks.HttpContextStorage
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.Extensions.Primitives;
    using Newtonsoft.Json;
    using Pipes;

    public class HttpContextStorage : EmptyHttpContextStorage
    {
        private readonly IQueryArgumentKeys keys = new QueryArgumentKeys();
        private readonly Dictionary<string, StringValues> queryValues;

        public HttpContextStorage()
        {
            this.queryValues = new Dictionary<string, StringValues>();
            this.Value.Request.Query = new QueryCollection(this.queryValues);
        }

        public HttpContextStorage SetOrderByValue(string orderBy) =>
            this.SetValue(this.keys.OrderBy, orderBy);

        public HttpContextStorage SetFilterValue(string property, string filter) =>
            this.SetFilterValue(new Dictionary<string, string> { [property] = filter });

        public HttpContextStorage SetFilterValue(Dictionary<string, string> filters) =>
            this.SetFilterValue(JsonConvert.SerializeObject(filters));

        public HttpContextStorage SetFilterValue(string filter) =>
            this.SetValue(this.keys.Filter, filter);

        public HttpContextStorage SetPageValue(string page) => this.SetValue(this.keys.Page, page);

        public HttpContextStorage SetEntriesPerPageValue(string entriesPerPage) =>
            this.SetValue(this.keys.EntriesPerPage, entriesPerPage);

        public HttpContextStorage SetSearchValue(string search) =>
            this.SetValue(this.keys.Search, search);

        private HttpContextStorage SetValue(string key, string value)
        {
            this.queryValues[key] = new StringValues(value);
            return this;
        }
    }
}
