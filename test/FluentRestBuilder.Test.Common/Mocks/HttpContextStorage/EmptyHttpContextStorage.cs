// <copyright file="EmptyHttpContextStorage.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Common.Mocks.HttpContextStorage
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Storage;

    public class EmptyHttpContextStorage : ScopedStorage<HttpContext>
    {
        public EmptyHttpContextStorage()
        {
            this.Value = new DefaultHttpContext
            {
                Request =
                {
                    Query = QueryCollection.Empty
                }
            };
        }
    }
}
