// <copyright file="IncludeAliasTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Test.Operators.Queryable
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Mocks;
    using Mocks.EntityFramework;
    using Xunit;

    public class IncludeAliasTest
    {
        private readonly PersistantDatabase database = new PersistantDatabase();

        [Fact]
        public async Task TestInclude()
        {
            var parents = this.database.CreateParentsWithChildren(10);
            var result = await Observable.Single(this.database.Create())
                .Map(c => c.Set<Parent>())
                .Include(p => p.Children)
                .ToListAsync();
            Assert.Equal(parents, result, new PropertyComparer<Parent>());
            foreach (var tuple in parents.Zip(result, Tuple.Create))
            {
                Assert.Equal(
                    tuple.Item1.Children, tuple.Item2.Children, new PropertyComparer<Child>());
            }
        }
    }
}
