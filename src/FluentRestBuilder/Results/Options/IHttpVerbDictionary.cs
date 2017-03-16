// <copyright file="IHttpVerbDictionary.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Results.Options
{
    using System.Collections.Generic;

    public interface IHttpVerbDictionary : IReadOnlyDictionary<HttpVerb, string>
    {
    }
}
