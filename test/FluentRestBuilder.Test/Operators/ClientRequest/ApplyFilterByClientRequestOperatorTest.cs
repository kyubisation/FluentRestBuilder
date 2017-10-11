// <copyright file="ApplyFilterByClientRequestOperatorTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Operators.ClientRequest
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentRestBuilder.Operators.ClientRequest.FilterExpressions;
    using FluentRestBuilder.Operators.ClientRequest.Interpreters;
    using FluentRestBuilder.Operators.ClientRequest.Interpreters.Requests;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Mocks.EntityFramework;
    using Xunit;

    public class ApplyFilterByClientRequestOperatorTest
    {
        private readonly PersistantDatabase database = new PersistantDatabase();
        private readonly MockFilterByClientRequestInterpreter interpreter =
            new MockFilterByClientRequestInterpreter();

        private readonly ServiceProvider provider;

        public ApplyFilterByClientRequestOperatorTest()
        {
            this.provider = new ServiceCollection()
                .AddSingleton<IFilterByClientRequestInterpreter>(this.interpreter)
                .BuildServiceProvider();
        }

        [Fact]
        public async Task TestEmptyUseCase()
        {
            var entities = this.database.CreateEnumeratedEntities(10);
            var filterDictionary = new FilterExpressionProviderDictionary<Entity>(this.provider);
            var resultEntities = await Observable.Single(
                this.database.Create().Entities, this.provider)
                .ApplyFilterByClientRequest(filterDictionary)
                .ToList();
            Assert.Equal(entities, resultEntities, new PropertyComparer<Entity>());
        }

        [Fact]
        public async Task TestFilter()
        {
            var entities = this.database.CreateEnumeratedEntities(10);
            var entity = entities[3];
            var filterDictionary = new FilterExpressionProviderDictionary<Entity>(this.provider)
                .AddFilter(nameof(Entity.Name), (f, d) => d.AddEquals(e => e.Name == f));
            this.interpreter.Add(
                new FilterRequest(nameof(Entity.Name), FilterType.Equals, entity.Name));
            var resultEntities = await Observable.Single(
                    this.database.Create().Entities, this.provider)
                .ApplyFilterByClientRequest(filterDictionary)
                .ToList();
            Assert.Single(resultEntities);
            Assert.Equal(entity.Id, resultEntities.Single().Id);
        }

        private sealed class MockFilterByClientRequestInterpreter : IFilterByClientRequestInterpreter
        {
            private readonly List<FilterRequest> filterRequests = new List<FilterRequest>();

            public IEnumerable<FilterRequest> ParseRequestQuery(
                ICollection<string> supportedFilters) =>
                this.filterRequests;

            public void Add(FilterRequest request) => this.filterRequests.Add(request);
        }
    }
}
