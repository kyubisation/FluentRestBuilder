// <copyright file="MapToRestCollectionOperatorTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage.Test.Operators
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentRestBuilder.Mocks;
    using FluentRestBuilder.Mocks.EntityFramework;
    using FluentRestBuilder.Operators.ClientRequest;
    using FluentRestBuilder.Storage;
    using HypertextApplicationLanguage.Links;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Xunit;

    public class MapToRestCollectionOperatorTest
    {
        private readonly PersistantDatabase database = new PersistantDatabase();

        private readonly IScopedStorage<PaginationInfo> paginationInfoStorage =
            new ScopedStorage<PaginationInfo>();

        private readonly ServiceProvider provider;

        public MapToRestCollectionOperatorTest()
        {
            this.provider = new ServiceCollection()
                .AddSingleton<IScopedStorage<HttpContext>>(new ScopedStorage<HttpContext>
                {
                    Value = new DefaultHttpContext
                    {
                        Request =
                        {
                            Host = HostString.FromUriComponent("test.com"),
                            PathBase = PathString.Empty,
                            Path = PathString.Empty,
                            QueryString = QueryString.Empty,
                            Scheme = "http",
                        },
                    },
                })
                .AddSingleton(this.paginationInfoStorage)
                .BuildServiceProvider();
        }

        [Fact]
        public async Task TestWithoutMetaInfo()
        {
            var entities = this.database.CreateEnumeratedEntities(50)
                .Take(10)
                .ToList();
            var result = await Observable.Single(entities, this.provider)
                .MapToRestCollection(e => new EntityResponse(e)) as RestEntityCollection;
            Assert.NotNull(result);
            Assert.Null(result.Limit);
            Assert.Null(result.Offset);
            Assert.Null(result.Total);
            Assert.Single(result._links);
            Assert.Equal(Link.Self, result._links.Keys.Single());
            Assert.Equal(
                entities.Select(e => new EntityResponse(e)),
                result._embedded.Values.Single() as IEnumerable<EntityResponse>,
                new PropertyComparer<EntityResponse>());
        }

        [Fact]
        public async Task TestWithMetaInfo()
        {
            var entities = this.database.CreateEnumeratedEntities(50)
                .Take(10)
                .ToList();
            this.paginationInfoStorage.Value = new PaginationInfo(50, 0, 10);
            var result = await Observable.Single(entities, this.provider)
                .MapToRestCollection(e => new EntityResponse(e)) as RestEntityCollection;
            Assert.NotNull(result);
            Assert.Equal(this.paginationInfoStorage.Value.Limit, result.Limit);
            Assert.Equal(this.paginationInfoStorage.Value.Offset, result.Offset);
            Assert.Equal(this.paginationInfoStorage.Value.Total, result.Total);
            Assert.True(result._links.Count > 1);
            Assert.Contains(Link.Self, result._links.Keys);
            Assert.Equal(
                entities.Select(e => new EntityResponse(e)),
                result._embedded.Values.Single() as IEnumerable<EntityResponse>,
                new PropertyComparer<EntityResponse>());
        }
    }
}
