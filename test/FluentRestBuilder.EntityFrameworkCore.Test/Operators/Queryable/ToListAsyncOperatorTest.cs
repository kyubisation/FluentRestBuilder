// <copyright file="ToListAsyncOperatorTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Test.Operators.Queryable
{
    using System.Threading.Tasks;
    using Mocks;
    using Mocks.EntityFramework;
    using Xunit;

    public class ToListAsyncOperatorTest
    {
        private readonly PersistantDatabase database = new PersistantDatabase();

        [Fact]
        public async Task TestToListAsync()
        {
            var generatedEntity = this.database
                .CreateEnumeratedEntities(10);
            var result = await Observable.Single(this.database.Create())
                .Map(c => c.Set<Entity>())
                .ToListAsync();
            Assert.Equal(generatedEntity, result, new PropertyComparer<Entity>());
        }
    }
}
