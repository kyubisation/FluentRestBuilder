﻿// <copyright file="CollectionTransformationPipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.Test.ChainPipes.CollectionTransformation
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Core.ChainPipes.CollectionTransformation;
    using Core.SourcePipes.EntityCollection;
    using Mocks;
    using Transformers.Hal;
    using Xunit;

    public class CollectionTransformationPipeTest : ScopedDbContextTestBase
    {
        [Fact]
        public async Task TestCollectionTransformation()
        {
            this.CreateEntities();
            var resultPipe = MockSourcePipe<IQueryable<Entity>>.CreateCompleteChain(
                this.Context.Entities,
                this.ServiceProvider,
                source => new CollectionTransformationPipe<Entity, string>(
                    e => e.Name, new MockLinkGenerator(), source));
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
