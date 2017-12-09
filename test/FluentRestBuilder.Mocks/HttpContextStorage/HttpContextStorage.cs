// <copyright file="HttpContextStorage.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Mocks.HttpContextStorage
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.Extensions.Primitives;

    public class HttpContextStorage : EmptyHttpContextStorage
    {
        private readonly Dictionary<string, StringValues> queryValues;

        public HttpContextStorage()
        {
            this.queryValues = new Dictionary<string, StringValues>();
            this.Value.Request.Query = new QueryCollection(this.queryValues);
        }

        public HttpContextStorage SetOrderByValue(string sort) => this.SetValue("sort", sort);

        public HttpContextStorage SetOffsetValue(string offset) => this.SetValue("offset", offset);

        public HttpContextStorage SetLimitValue(string limit) => this.SetValue("limit", limit);

        public HttpContextStorage SetSearchValue(string search) => this.SetValue("q", search);

        public HttpContextStorage SetValue(string key, string value)
        {
            this.queryValues[key] = new StringValues(value);
            return this;
        }

        public HttpContextStorage SetRangeHeader(string value)
        {
            this.Value.Request.Headers["Range"] = new StringValues(value);
            return this;
        }
    }
}
