// <copyright file="RestEntity.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.Transformers.Hal
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public abstract class RestEntity
    {
        [JsonProperty("_links", NullValueHandling = NullValueHandling.Ignore)]
        public IDictionary<string, object> Links { get; set; }

        [JsonProperty("_embedded", NullValueHandling = NullValueHandling.Ignore)]
        public IDictionary<string, object> Embedded { get; set; }
    }
}
