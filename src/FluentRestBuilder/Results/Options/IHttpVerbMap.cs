// <copyright file="IHttpVerbMap.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Results.Options
{
    using System.Collections.Generic;

    public interface IHttpVerbMap : IReadOnlyDictionary<HttpVerb, string>
    {
    }
}
