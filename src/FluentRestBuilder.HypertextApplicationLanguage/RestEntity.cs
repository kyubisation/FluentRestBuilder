// <copyright file="RestEntity.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public abstract class RestEntity : IRestEntity
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IDictionary<string, object> _links { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IDictionary<string, object> _embedded { get; set; }
    }
}
