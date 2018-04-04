// <copyright file="HttpContextProviderAttributeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Filters
{
    using FluentRestBuilder.Filters;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Storage;
    using Xunit;

    public class HttpContextProviderAttributeTest
    {
        [Fact]
        public void TestSettingContext()
        {
            var serviceProvider = new ServiceCollection()
                .AddFluentRestBuilder()
                .Services
                .BuildServiceProvider()
                .CreateScope()
                .ServiceProvider;
            var controller = new MockController(serviceProvider);
            var context = new MockActionExecutingContext(controller);
            var filter = new HttpContextProviderAttribute();
            filter.OnActionExecuting(context);
            var storage = serviceProvider.GetService<IScopedStorage<HttpContext>>();
            Assert.Same(controller.HttpContext, storage.Value);
        }
    }
}
