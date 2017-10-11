// <copyright file="IRestEntity.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable InconsistentNaming
namespace FluentRestBuilder.HypertextApplicationLanguage
{
    using System.Collections.Generic;

    public interface IRestEntity
    {
        /// <summary>
        /// Gets or sets the links in relation to the current resource.
        /// <para>
        /// This is expected to serialize to "_links".
        /// If Newtonsoft.Json is used for serialization, use
        /// [JsonProperty("_links", NullValueHandling = NullValueHandling.Ignore)]
        /// for this property.
        /// </para>
        /// </summary>
        IDictionary<string, object> Links { get; set; }

        /// <summary>
        /// Gets or sets the embedded resources for the current resource.
        /// <para>
        /// This is expected to serialize to "_embedded".
        /// If Newtonsoft.Json is used for serialization, use
        /// [JsonProperty("_embedded", NullValueHandling = NullValueHandling.Ignore)]
        /// for this property.
        /// </para>
        /// </summary>
        IDictionary<string, object> Embedded { get; set; }
    }
}