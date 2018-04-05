// <copyright file="DeleteEntityOperatorTest.cs" company="Kyubisation">
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

    public class DeleteEntityOperatorTest
    {
        private readonly PersistantDatabase database = new PersistantDatabase();

        private readonly ServiceProvider provider;

        public DeleteEntityOperatorTest()
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
            await Observable.Single(entity, this.provider)
                .DeleteEntity();
            var count = this.database
                .Create()
                .Entities
                .Count();
            Assert.Equal(0, count);
        }
    }
}
