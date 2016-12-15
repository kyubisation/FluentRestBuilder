// <copyright file="CollectionMappingPipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Pipes.CollectionTransformation
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Common;
    using Common.Mocks;
    using FluentRestBuilder.Common;
    using FluentRestBuilder.Pipes.CollectionMapping;
    using Hal;
    using Storage;
    using Xunit;

    public class CollectionMappingPipeTest : ScopedDbContextTestBase
    {
        [Fact]
        public async Task TestCollectionTransformation()
        {
            this.CreateEntities();
            var resultPipe = MockSourcePipe<IQueryable<Entity>>.CreateCompleteChain(
                this.Context.Entities,
                this.ServiceProvider,
                source => new CollectionMappingPipe<Entity, string>(
                    e => e.Name, new MockLinkGenerator(), new ScopedStorage<PaginationMetaInfo>(), source));
            await resultPipe.Execute();
            Assert.NotNull(resultPipe.Input);
            Assert.Contains("items", resultPipe.Input.Embedded.Keys);
            Assert.IsAssignableFrom<IEnumerable<string>>(resultPipe.Input.Embedded["items"]);
        }

        private class MockLinkGenerator : IRestCollectionLinkGenerator
        {
            public IEnumerable<NamedLink> GenerateLinks(PaginationMetaInfo paginationMetaInfo) =>
                Enumerable.Empty<NamedLink>();
        }
    }
}
