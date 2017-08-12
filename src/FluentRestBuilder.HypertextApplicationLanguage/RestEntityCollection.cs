// <copyright file="RestEntityCollection.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage
{
    using System.Collections.Generic;

    public class RestEntityCollection : RestEntity
    {
        public RestEntityCollection()
        {
            this._links = new Dictionary<string, object>();
            this._embedded = new Dictionary<string, object>();
        }
    }
}
