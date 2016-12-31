// <copyright file="CollectionMappingPipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage.Test.Pipes.CollectionMapping
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentRestBuilder.Pipes;
    using FluentRestBuilder.Test.Common;
    using FluentRestBuilder.Test.Common.Mocks;
    using HypertextApplicationLanguage.Pipes.CollectionMapping;
    using Links;
    using Microsoft.Extensions.DependencyInjection;
    using Sources.Source;
    using Storage;
    using Xunit;

    public class CollectionMappingPipeTest : ScopedDbContextTestBase
    {
        [Fact]
        public async Task TestCollectionTransformation()
        {
            this.CreateEntities();
            var result = await new Source<IQueryable<Entity>>(this.Context.Entities, this.ServiceProvider)
                .MapToRestCollection(e => e.Name)
                .ToObjectResultOrDefault();
            Assert.NotNull(result);
            Assert.Contains("items", result.Embedded.Keys);
            Assert.IsAssignableFrom<IEnumerable<string>>(result.Embedded["items"]);
        }

        protected override void Setup(IServiceCollection services)
        {
            base.Setup(services);
            services.AddTransient<ICollectionMappingPipeFactory<Entity, string>>(
                p => new CollectionMappingPipeFactory<Entity, string>(
                    new MockLinkGenerator(),
                    new ScopedStorage<PaginationMetaInfo>(),
                    new QueryableTransformer<Entity>()));
        }

        private class MockLinkGenerator : IRestCollectionLinkGenerator
        {
            public IEnumerable<NamedLink> GenerateLinks(PaginationMetaInfo paginationMetaInfo) =>
                Enumerable.Empty<NamedLink>();
        }
    }
}
