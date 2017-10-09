// <copyright file="LoadCollectionAliasTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Test.Operators
{
    using System.Linq;
    using System.Threading.Tasks;
    using FluentRestBuilder.Storage;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Mocks.EntityFramework;
    using Xunit;

    public class LoadCollectionAliasTest
    {
        private readonly PersistantDatabase database = new PersistantDatabase();

        private readonly ServiceProvider provider;

        public LoadCollectionAliasTest()
        {
            this.provider = new ServiceCollection()
                .AddSingleton<IScopedStorage<DbContext>>(
                    s => new ScopedStorage<DbContext> { Value = this.database.Create() })
                .BuildServiceProvider();
        }

        [Fact]
        public async Task TestUseCase()
        {
            var expectedParent = this.database.CreateParentsWithChildren(5)
                .Skip(2)
                .First();
            var context = this.provider.GetService<IScopedStorage<DbContext>>();
            var parent = context.Value.Set<Parent>().Single(p => p.Id == expectedParent.Id);
            Assert.Null(parent.Children);
            var resultParent = await Observable.Single(parent, this.provider)
                .LoadCollection(p => p.Children);
            Assert.NotNull(resultParent.Children);
            Assert.Equal(
                expectedParent.Children, resultParent.Children, new PropertyComparer<Child>());
        }
    }
}
