// <copyright file="MockActionExecutingContext.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Mocks
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Routing;

    public class MockActionExecutingContext : ActionExecutingContext
    {
        public MockActionExecutingContext(object controller)
            : base(
                  Create(),
                  new List<IFilterMetadata>(),
                  new Dictionary<string, object>(),
                  controller)
        {
        }

        private static ActionContext Create() =>
            new ActionContext(new DefaultHttpContext(), new RouteData(), new ActionDescriptor());
    }
}
