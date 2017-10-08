// <copyright file="HttpContextProviderAttributeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Filters
{
    using System.Collections.Generic;
    using FluentRestBuilder.Filters;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Storage;
    using Xunit;

    public class HttpContextProviderAttributeTest
    {
        [Fact]
        public void TestMissingController()
        {
            var context = new MockActionExecutingContext(new object());
            var filter = new HttpContextProviderAttribute();
            Assert.Throws<FilterRequiresControllerException>(
                () => filter.OnActionExecuting(context));
        }

        [Fact]
        public void TestSettingContext()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddFluentRestBuilder();
            var scope = serviceCollection.BuildServiceProvider()
                .CreateScope();
            var controller = new MockController(scope.ServiceProvider);
            var context = new MockActionExecutingContext(controller);
            var filter = new HttpContextProviderAttribute();
            filter.OnActionExecuting(context);
            var storage = scope.ServiceProvider.GetService<IScopedStorage<HttpContext>>();
            Assert.Same(controller.HttpContext, storage.Value);
        }
    }
}
