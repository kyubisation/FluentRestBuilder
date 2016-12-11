// <copyright file="RestEntityCollection.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Pipes.CollectionTransformation
{
    using System.Collections.Generic;
    using Transformers.Hal;

    public class RestEntityCollection : RestEntity
    {
        public RestEntityCollection()
        {
            this.Links = new Dictionary<string, object>();
            this.Embedded = new Dictionary<string, object>();
        }
    }
}
