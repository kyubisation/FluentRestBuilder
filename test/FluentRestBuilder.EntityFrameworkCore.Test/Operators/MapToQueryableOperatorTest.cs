// <copyright file="MapToQueryableOperatorTest.cs" company="Kyubisation">
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

    public class MapToQueryableOperatorTest
    {
        private readonly PersistantDatabase database = new PersistantDatabase();

        private readonly ServiceProvider provider;

        public MapToQueryableOperatorTest()
        {
            this.provider = new ServiceCollection()
                .AddSingleton<IScopedStorage<DbContext>>(
                    s => new ScopedStorage<DbContext> { Value = this.database.Create() })
                .BuildServiceProvider();
        }

        [Fact]
        public async Task TestUseCase()
        {
            const string expected = "Name 2";
            this.database.CreateSimilarEntities(3, "Name 1");
            var entities = this.database.CreateSimilarEntities(3, expected);
            this.database.CreateSimilarEntities(3, "Name 3");
            var resultEntities = await Observable.Single(expected, this.provider)
                .MapToQueryable((s, c) => c.Set<Entity>().Where(e => e.Name == expected))
                .ToList();
            Assert.Equal(entities, resultEntities, new PropertyComparer<Entity>());
        }
    }
}
