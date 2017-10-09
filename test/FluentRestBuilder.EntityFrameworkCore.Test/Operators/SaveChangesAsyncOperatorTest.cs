// <copyright file="SaveChangesAsyncOperatorTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Test.Operators
{
    using System.Linq;
    using System.Threading.Tasks;
    using FluentRestBuilder.Storage;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks.EntityFramework;
    using Xunit;

    public class SaveChangesAsyncOperatorTest
    {
        private readonly PersistantDatabase database = new PersistantDatabase();

        private readonly ServiceProvider provider;

        public SaveChangesAsyncOperatorTest()
        {
            this.provider = new ServiceCollection()
                .AddSingleton<IScopedStorage<DbContext>>(
                    s => new ScopedStorage<DbContext> { Value = this.database.Create() })
                .BuildServiceProvider();
        }

        [Fact]
        public async Task TestUseCase()
        {
            var generatedEntity = this.database
                .CreateEnumeratedEntities(1)
                .Single();
            var context = this.provider.GetService<IScopedStorage<DbContext>>();
            var entity = context.Value.Find<Entity>(generatedEntity.Id);
            const string expectedName = "expectedName";
            Assert.NotEqual(expectedName, entity.Name);
            entity.Name = expectedName;
            await Observable.Single(entity, this.provider)
                .SaveChangesAsync();
            using (var newContext = this.database.Create())
            {
                var resultEntity = newContext.Entities.Single();
                Assert.Equal(expectedName, resultEntity.Name);
            }
        }
    }
}
