// <copyright file="UrlHelperProviderAttributeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Filters
{
    using FluentRestBuilder.Filters;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Storage;
    using Xunit;

    public class UrlHelperProviderAttributeTest
    {
        [Fact]
        public void TestMissingController()
        {
            var context = new MockActionExecutingContext(new object());
            var filter = new UrlHelperProviderAttribute();
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
            var filter = new UrlHelperProviderAttribute();
            filter.OnActionExecuting(context);
            var storage = scope.ServiceProvider.GetService<IScopedStorage<IUrlHelper>>();
            Assert.Same(controller.Url, storage.Value);
        }
    }
}
