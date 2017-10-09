// <copyright file="WithEntityEntryOperatorTest.cs" company="Kyubisation">
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

    public class WithEntityEntryOperatorTest
    {
        private readonly PersistantDatabase database = new PersistantDatabase();

        private readonly ServiceProvider provider;

        public WithEntityEntryOperatorTest()
        {
            this.provider = new ServiceCollection()
                .AddSingleton<IScopedStorage<DbContext>>(
                    s => new ScopedStorage<DbContext> { Value = this.database.Create() })
                .BuildServiceProvider();
        }

        [Fact]
        public async Task TestUseCase()
        {
            var id = this.database.CreateEnumeratedEntities(5)
                .Skip(2)
                .First()
                .Id;
            var context = this.provider.GetService<IScopedStorage<DbContext>>();
            var entity = context.Value.Set<Entity>().Single(e => e.Id == id);
            Entity expectedEntity;
            using (var newContext = this.database.Create())
            {
                expectedEntity = newContext.Entities.Single(e => e.Id == id);
                expectedEntity.Name = "new value";
                await newContext.SaveChangesAsync();
            }

            Assert.NotEqual(expectedEntity, entity, new PropertyComparer<Entity>());
            var resultEntity = await Observable.Single(entity, this.provider)
                .WithEntityEntry(e => e.Reload());
            Assert.Equal(expectedEntity, resultEntity, new PropertyComparer<Entity>());
        }
    }
}
