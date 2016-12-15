// <copyright file="RestCollectionSearchTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Test.RestCollectionMutators.Search
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EntityFrameworkCore.RestCollectionMutators.Search;
    using FluentRestBuilder.Common;
    using FluentRestBuilder.Test.Common.Mocks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Primitives;
    using Xunit;

    public class RestCollectionSearchTest : ScopedDbContextTestBase
    {
        public RestCollectionSearchTest()
        {
            this.CreateEntities();
        }

        [Fact]
        public async Task TestSearchFilterCreation()
        {
            const string namePart = "me2";
            var search = new RestCollectionSearch<Entity>(
                this.CreateQueryCollection(new[] { namePart }),
                this.Factory,
                new QueryArgumentKeys());
            var result = await search.Apply(this.Context.Entities)
                .ToListAsync();
            Assert.Equal(1, result.Count);
            Assert.Contains(namePart, result.First().Name);
        }

        [Fact]
        public async Task TestMultipleSearchFilterCreation()
        {
            var nameParts = new[] { "me2", "me3", "notcontained" };
            var search = new RestCollectionSearch<Entity>(
                this.CreateQueryCollection(nameParts),
                this.Factory,
                new QueryArgumentKeys());
            var result = await search.Apply(this.Context.Entities)
                .ToListAsync();
            Assert.Equal(2, result.Count);
        }

        private IQueryCollection CreateQueryCollection(string[] searchables)
        {
            return new QueryCollection(new Dictionary<string, StringValues>
            {
                { new QueryArgumentKeys().Search, new StringValues(searchables) }
            });
        }
    }
}