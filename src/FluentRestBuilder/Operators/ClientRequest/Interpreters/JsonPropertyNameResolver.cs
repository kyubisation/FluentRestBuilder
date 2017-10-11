// <copyright file="JsonPropertyNameResolver.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators.ClientRequest.Interpreters
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json.Serialization;

    public class JsonPropertyNameResolver : IJsonPropertyNameResolver
    {
        private readonly NamingStrategy namingStrategy;

        public JsonPropertyNameResolver(IOptions<MvcJsonOptions> options)
        {
            var resolver = options.Value.SerializerSettings.ContractResolver;
            this.namingStrategy = resolver is DefaultContractResolver defaultContractResolver
                                  && defaultContractResolver.NamingStrategy != null
                ? defaultContractResolver.NamingStrategy
                : new CamelCaseNamingStrategy();
        }

        public string Resolve(string propertyName) =>
            this.namingStrategy.GetPropertyName(propertyName, false);
    }
}
