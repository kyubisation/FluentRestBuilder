// <copyright file="UrlHelperMock.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Mocks
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Microsoft.AspNetCore.Routing;

    public class UrlHelperMock : UrlHelper
    {
        public UrlHelperMock()
            : base(CreateContext())
        {
        }

        private static ActionContext CreateContext() =>
            new ActionContext
            {
                HttpContext = new DefaultHttpContext(),
                RouteData = new RouteData(),
            };
    }
}
