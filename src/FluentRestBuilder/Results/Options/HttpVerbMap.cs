// <copyright file="HttpVerbMap.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Results.Options
{
    using System.Collections.Generic;

    public class HttpVerbMap : Dictionary<HttpVerb, string>, IHttpVerbMap
    {
        public HttpVerbMap()
        {
            this[HttpVerb.Delete] = "DELETE";
            this[HttpVerb.Get] = "GET, HEAD";
            this[HttpVerb.Patch] = "PATCH";
            this[HttpVerb.Post] = "POST";
            this[HttpVerb.Put] = "PUT";
        }
    }
}