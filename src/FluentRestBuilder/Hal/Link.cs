// <copyright file="Link.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Hal
{
    using Newtonsoft.Json;

    public class Link
    {
        public const string Self = "self";

        public Link(string href)
        {
            this.Href = href;
        }

        public string Href { get; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool Templated { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Deprecation { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Profile { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Hreflang { get; set; }
    }
}
