// <copyright file="ReloadEntityAliasTest.cs" company="Kyubisation">
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

    public class ReloadEntityAliasTest
    {
        private readonly PersistantDatabase database = new PersistantDatabase();

        private readonly ServiceProvider provider;

        public ReloadEntityAliasTest()
        {
            this.provider = new ServiceCollection()
                .AddSingleton<IScopedStorage<DbContext>>(
                    s => new ScopedStorage<DbContext> { Value = this.database.Create() })
                .BuildServiceProvider();
        }

        [Fact]
        public async Task TestReloadEntity()
        {
            var generatedEntity = this.database
                .CreateEnumeratedEntities(1)
                .Single();
            var result = await Observable.Single(generatedEntity.Id, this.provider)
                .MapToQueryable((id, context) => context.Set<Entity>().Where(e => e.Id == id))
                .SingleAsync()
                .Do(e => Assert.NotSame(generatedEntity, e))
                .Do(e => Assert.Equal(generatedEntity, e, new PropertyComparer<Entity>()))
                .Do(e => e.Name = e.Name + "test")
                .Do(e => Assert.NotEqual(generatedEntity, e, new PropertyComparer<Entity>()))
                .ReloadEntity();
            Assert.Equal(generatedEntity, result, new PropertyComparer<Entity>());
        }
    }
}
