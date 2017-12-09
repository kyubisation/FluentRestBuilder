// <copyright file="SingleAndFirstTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Test.Operators.Queryable
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Mocks;
    using Mocks.EntityFramework;
    using Xunit;

    public class SingleAndFirstTest
    {
        private readonly PersistantDatabase database = new PersistantDatabase();

        public static IEnumerable<object[]> Selectors() =>
            new List<Func<IProviderObservable<IQueryable<Entity>>, IProviderObservable<Entity>>>
                {
                    o => o.SingleAsync(),
                    o => o.SingleAsync(e => e.Id == 1),
                    o => o.SingleOrDefaultAsync(),
                    o => o.SingleOrDefaultAsync(e => e.Id == 1),
                    o => o.FirstAsync(),
                    o => o.FirstAsync(e => e.Id == 1),
                    o => o.FirstOrDefaultAsync(),
                    o => o.FirstOrDefaultAsync(e => e.Id == 1),
                }
                .Select(f => new object[] { f });

        [Theory]
        [MemberData(nameof(Selectors))]
        public async Task TestValidationWithErrors(
            Func<IProviderObservable<IQueryable<Entity>>, IProviderObservable<Entity>> function)
        {
            var entity = this.database.CreateEnumeratedEntities(1).Single();
            using (var context = this.database.Create())
            {
                var result = await function(Observable.Single(context.Entities));
                Assert.Equal(entity, result, new PropertyComparer<Entity>());
            }
        }
    }
}
