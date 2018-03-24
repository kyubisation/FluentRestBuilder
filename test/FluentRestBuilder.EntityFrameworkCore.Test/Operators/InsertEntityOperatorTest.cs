// <copyright file="InsertEntityOperatorTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Test.Operators
{
    using System.Linq;
    using System.Threading.Tasks;
    using EntityFrameworkCore.Operators.Exceptions;
    using FluentRestBuilder.Storage;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Mocks.EntityFramework;
    using Xunit;

    public class InsertEntityOperatorTest
    {
        private readonly PersistantDatabase database = new PersistantDatabase();

        private readonly ServiceProvider provider;

        public InsertEntityOperatorTest()
        {
            this.provider = new ServiceCollection()
                .AddSingleton<IScopedStorage<DbContext>>(
                    s => new ScopedStorage<DbContext> { Value = this.database.Create() })
                .BuildServiceProvider();
        }

        [Fact]
        public async Task TestUseCase()
        {
            var entity = new Entity { Id = 1, Name = "name", Description = "description" };
            await Observable.Single(entity, this.provider)
                .InsertEntity();
            var resultEntity = this.database.Create()
                .Set<Entity>()
                .Single();
            Assert.Equal(entity, resultEntity, new PropertyComparer<Entity>());
        }

        [Fact]
        public async Task TestConcurrencyException()
        {
            await Assert.ThrowsAsync<ConflictException>(
                async () => await Observable.Throw<Entity>(new MockDbUpdateConcurrencyException())
                    .InsertEntity());
        }
    }
}
