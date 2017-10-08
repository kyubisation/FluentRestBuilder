// <copyright file="MockController.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Mocks
{
    using System;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class MockController : ControllerBase
    {
        public MockController(IServiceProvider serviceProvider = null)
        {
            this.ControllerContext.HttpContext = new DefaultHttpContext
            {
                RequestServices = serviceProvider,
            };
            this.Url = new UrlHelperMock();
        }
    }
}
