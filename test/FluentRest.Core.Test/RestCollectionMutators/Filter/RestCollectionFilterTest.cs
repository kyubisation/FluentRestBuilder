namespace KyubiCode.FluentRest.Test.RestCollectionMutators.Filter
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentRest.RestCollectionMutators;
    using FluentRest.RestCollectionMutators.Filter;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Primitives;
    using Mocks;
    using Xunit;

    public class RestCollectionFilterTest : ScopedDbContextTestBase
    {
        public RestCollectionFilterTest()
        {
            this.CreateEntities();
        }

        [Fact]
        public async Task TestSearchFilterCreation()
        {
            const string namePart = "me2";
            var filterDictionary = new Dictionary<string, StringValues>
            {
                {
                    new QueryArgumentKeys().Filter,
                    new StringValues(new[] { $"{{'{nameof(Entity.Name)}':'{namePart}'}}" })
                }
            };
            var filter = new RestCollectionFilter<Entity>(
                new QueryCollection(filterDictionary),
                this.Factory,
                new QueryArgumentKeys());
            var result = await filter.Apply(this.Context.Entities)
                .ToListAsync();
            Assert.Equal(1, result.Count);
            Assert.Contains(namePart, result.First().Name);
        }
    }
}
