namespace KyubiCode.FluentRest.Transformers.Hal
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
