﻿// <copyright file="RestEntityCollection.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class RestEntityCollection : RestEntity
    {
        public RestEntityCollection()
        {
            this._links = new Dictionary<string, object>();
            this._embedded = new Dictionary<string, object>();
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Total { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Offset { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Limit { get; set; }
    }
}
