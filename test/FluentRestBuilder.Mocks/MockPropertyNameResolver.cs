// <copyright file="MockPropertyNameResolver.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Mocks
{
    using Newtonsoft.Json.Serialization;
    using Operators.ClientRequest.Interpreters;

    public class MockPropertyNameResolver : IJsonPropertyNameResolver
    {
        private readonly NamingStrategy namingStrategy = new CamelCaseNamingStrategy();

        public string Resolve(string propertyName) =>
            this.namingStrategy.GetPropertyName(propertyName, false);
    }
}
