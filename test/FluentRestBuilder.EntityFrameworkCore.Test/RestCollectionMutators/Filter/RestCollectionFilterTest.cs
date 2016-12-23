// <copyright file="RestCollectionFilterTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Test.RestCollectionMutators.Filter
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EntityFrameworkCore.RestCollectionMutators.Filter;
    using FluentRestBuilder.Common;
    using FluentRestBuilder.Test.Common.Mocks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Primitives;
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
                this.CreateAccessor(filterDictionary),
                this.Factory,
                new QueryArgumentKeys());
            var result = await filter.Apply(this.Context.Entities)
                .ToListAsync();
            Assert.Equal(1, result.Count);
            Assert.Contains(namePart, result.First().Name);
        }

        private IHttpContextAccessor CreateAccessor(Dictionary<string, StringValues> filters)
        {
            return new HttpContextAccessor
            {
                HttpContext = new DefaultHttpContext
                {
                    Request =
                    {
                        Query = new QueryCollection(filters)
                    }
                }
            };
        }
    }
}
