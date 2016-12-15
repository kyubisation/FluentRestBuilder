// <copyright file="RestEntityCollection.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Hal
{
    using System.Collections.Generic;

    public class RestEntityCollection : RestEntity
    {
        public RestEntityCollection()
        {
            this.Links = new Dictionary<string, object>();
            this.Embedded = new Dictionary<string, object>();
        }
    }
}
