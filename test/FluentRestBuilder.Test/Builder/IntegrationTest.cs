// <copyright file="IntegrationTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Builder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.ApplicationParts;
    using Microsoft.AspNetCore.Mvc.Internal;
    using Microsoft.Extensions.DependencyInjection;
    using Storage;
    using Xunit;

    public class IntegrationTest
    {
        [Theory]
        [ClassData(typeof(Providers))]
        public void TestRegistration(IServiceProvider provider)
        {
            var token = new object();
            for (var i = 0; i < 2; i++)
            {
                using (var scope = provider.CreateScope())
                {
                    var storage = scope.ServiceProvider.GetService<IScopedStorage<object>>();
                    Assert.NotNull(storage);
                    Assert.Null(storage.Value);
                    storage.Value = token;
                    var sameStorage = scope.ServiceProvider.GetService<IScopedStorage<object>>();
                    Assert.Same(storage, sameStorage);
                    Assert.Same(token, sameStorage.Value);
                }
            }
        }

        public class Providers : List<object[]>
        {
            public Providers()
            {
                var collections = new[] { ServiceCollection(), MvcBuilder(), MvcCoreBuilder() }
                    .Select(c => new object[] { c });
                this.AddRange(collections);
            }

            private static IServiceProvider ServiceCollection()
            {
                var services = new ServiceCollection();
                services.AddFluentRestBuilder();
                return services.BuildServiceProvider();
            }

            private static IServiceProvider MvcBuilder()
            {
                var services = new ServiceCollection();
                var mvc = new MvcBuilder(services, new ApplicationPartManager());
                mvc.AddFluentRestBuilder();
                return services.BuildServiceProvider();
            }

            private static IServiceProvider MvcCoreBuilder()
            {
                var services = new ServiceCollection();
                var mvc = new MvcCoreBuilder(services, new ApplicationPartManager());
                mvc.AddFluentRestBuilder();
                return services.BuildServiceProvider();
            }
        }
    }
}
