// <copyright file="DbContextScopedStorageTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Test.Storage
{
    using EntityFrameworkCore.Storage;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks.EntityFramework;
    using Xunit;

    public class DbContextScopedStorageTest
    {
        [Fact]
        public void TestSameContext()
        {
            var provider = new ServiceCollection()
                .AddScoped(p => new MockDbContext())
                .BuildServiceProvider();
            using (var scope = provider.CreateScope())
            {
                var storage = new DbContextScopedStorage<MockDbContext>(scope.ServiceProvider);
                var context = scope.ServiceProvider.GetService<MockDbContext>();
                Assert.Same(context, storage.Value);
            }

            using (var scope = provider.CreateScope())
            {
                var storage = new DbContextScopedStorage<MockDbContext>(scope.ServiceProvider);
                var storageContext = storage.Value;
                var context = scope.ServiceProvider.GetService<MockDbContext>();
                Assert.Same(storageContext, context);
            }
        }
    }
}
