// <copyright file="AsNoTrackingAliasTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Test.Operators.Queryable
{
    using System.Threading.Tasks;
    using Mocks;
    using Mocks.EntityFramework;
    using Xunit;

    public class AsNoTrackingAliasTest
    {
        private readonly PersistantDatabase database = new PersistantDatabase();

        [Fact]
        public async Task TestAsNoTrackingAlias()
        {
            var generatedEntity = this.database
                .CreateEnumeratedEntities(10);
            var result = await Observable.Single(this.database.Create())
                .Map(c => c.Set<Entity>())
                .AsNoTracking()
                .ToListAsync();
            Assert.Equal(generatedEntity, result, new PropertyComparer<Entity>());
        }
    }
}
