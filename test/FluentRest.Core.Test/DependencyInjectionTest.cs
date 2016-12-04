// <copyright file="DependencyInjectionTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.Test
{
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Xunit;

    public class DependencyInjectionTest
    {
        [Fact]
        public void TestScopeFactory()
        {
            var provider = new ServiceCollection()
                .AddScoped(s => new MockDbContext())
                .BuildServiceProvider();

            var scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();
            MockDbContext context;
            using (var scope = scopeFactory.CreateScope())
            {
                context = scope.ServiceProvider.GetService<MockDbContext>();
                Assert.NotNull(context);
            }

            Assert.NotSame(context, provider.GetService<MockDbContext>());
        }
    }
}
