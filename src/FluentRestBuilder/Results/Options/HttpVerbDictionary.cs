// <copyright file="HttpVerbDictionary.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Results.Options
{
    using System.Collections.Generic;

    public class HttpVerbDictionary : Dictionary<HttpVerb, string>, IHttpVerbDictionary
    {
        public HttpVerbDictionary()
        {
            this[HttpVerb.Delete] = "DELETE";
            this[HttpVerb.Get] = "GET, HEAD";
            this[HttpVerb.Patch] = "PATCH";
            this[HttpVerb.Post] = "POST";
            this[HttpVerb.Put] = "PUT";
        }
    }
}