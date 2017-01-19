// <copyright file="HttpContextStorage.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Storage
{
    using Microsoft.AspNetCore.Http;

    public class HttpContextStorage : ScopedStorage<HttpContext>
    {
        public HttpContextStorage(IHttpContextAccessor httpContextAccessor)
        {
            this.Value = httpContextAccessor.HttpContext;
        }
    }
}
