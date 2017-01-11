// <copyright file="FluentRestBuilderCore.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Builder
{
    using System;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Storage;

    public class FluentRestBuilderCore : IFluentRestBuilderCore
    {
        public FluentRestBuilderCore(IServiceCollection services)
        {
            this.Services = services;
        }

        public IServiceCollection Services { get; }

        public IFluentRestBuilderCore RegisterStorage()
        {
            this.Services.TryAddScoped(typeof(IScopedStorage<>), typeof(ScopedStorage<>));
            this.Services.TryAddScoped(this.RegisterHttpContextScopedStorage);
            this.Services.TryAddScoped(this.RegisterUrlHelperScopedStorage);
            return this;
        }

        private IScopedStorage<HttpContext> RegisterHttpContextScopedStorage(
            IServiceProvider serviceProvider)
        {
            var accessor = serviceProvider.GetService<IHttpContextAccessor>();
            return new ScopedStorage<HttpContext> { Value = accessor?.HttpContext };
        }

        private IScopedStorage<IUrlHelper> RegisterUrlHelperScopedStorage(
            IServiceProvider serviceProvider)
        {
            var actionContextAccessor = serviceProvider.GetService<IActionContextAccessor>();
            if (actionContextAccessor == null)
            {
                return new ScopedStorage<IUrlHelper>();
            }

            var urlHelperFactory = serviceProvider.GetService<IUrlHelperFactory>();
            var urlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);
            return new ScopedStorage<IUrlHelper>
            {
                Value = urlHelper
            };
        }
    }
}
