// <copyright file="MockController.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Mocks
{
    using System;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Microsoft.Extensions.DependencyInjection;
    using Storage;

    public class MockController : ControllerBase, IDisposable
    {
        private IServiceScope scope;

        public MockController(IServiceProvider provider)
        {
            this.scope = provider.CreateScope();
            this.ControllerContext.HttpContext = new DefaultHttpContext
            {
                RequestServices = this.scope.ServiceProvider,
            };
            this.Url = new UrlHelper(this.ControllerContext);
        }

        public MockController SetupUrlHelperStorage()
        {
            var storage = this.scope.ServiceProvider.GetService<IScopedStorage<IUrlHelper>>();
            storage.Value = this.Url;
            return this;
        }

        public MockController SetupHttpContextStorage()
        {
            var storage = this.scope.ServiceProvider.GetService<IScopedStorage<HttpContext>>();
            storage.Value = this.HttpContext;
            return this;
        }

        public void Dispose()
        {
            if (this.scope == null)
            {
                return;
            }

            this.scope.Dispose();
            this.scope = null;
        }
    }
}
