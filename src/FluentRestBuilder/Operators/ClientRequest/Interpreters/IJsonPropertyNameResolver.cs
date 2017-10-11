// <copyright file="IJsonPropertyNameResolver.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators.ClientRequest.Interpreters
{
    public interface IJsonPropertyNameResolver
    {
        string Resolve(string propertyName);
    }
}
