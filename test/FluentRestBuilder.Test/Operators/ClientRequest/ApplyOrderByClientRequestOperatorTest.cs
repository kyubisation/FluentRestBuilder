// <copyright file="ApplyOrderByClientRequestOperatorTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Operators.ClientRequest
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentRestBuilder.Operators.ClientRequest;
    using FluentRestBuilder.Operators.ClientRequest.Interpreters;
    using FluentRestBuilder.Operators.ClientRequest.OrderByExpressions;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Mocks.EntityFramework;
    using Xunit;

    public class ApplyOrderByClientRequestOperatorTest
    {
        private readonly PersistantDatabase database = new PersistantDatabase();
        private readonly MockOrderByClientRequestInterpreter interpreter =
            new MockOrderByClientRequestInterpreter();

        private readonly ServiceProvider provider;

        public ApplyOrderByClientRequestOperatorTest()
        {
            this.provider = new ServiceCollection()
                .AddSingleton<IOrderByClientRequestInterpreter>(this.interpreter)
                .BuildServiceProvider();
        }

        [Fact]
        public async Task TestEmptyCase()
        {
            var entities = this.database.CreateEnumeratedEntities(10);
            var orderByDictionary = new OrderByExpressionDictionary<Entity>();
            var resultEntities = await Observable.Single(
                    this.database.Create().Entities, this.provider)
                .ApplyOrderByClientRequest(orderByDictionary)
                .ToList();
            Assert.Equal(entities, resultEntities, new PropertyComparer<Entity>());
        }

        [Fact]
        public async Task TestDescending()
        {
            var entities = this.database.CreateEnumeratedEntities(10)
                .OrderByDescending(e => e.Name)
                .ToList();
            var orderByDictionary = new OrderByExpressionDictionary<Entity>()
                .Add(nameof(Entity.Name), e => e.Name);
            this.interpreter
                .Add(new OrderByRequest(nameof(Entity.Name), OrderByDirection.Descending));
            var resultEntities = await Observable.Single(
                    this.database.Create().Entities, this.provider)
                .ApplyOrderByClientRequest(orderByDictionary)
                .ToList();
            Assert.Equal(entities, resultEntities, new PropertyComparer<Entity>());
        }

        [Fact]
        public async Task TestMixed()
        {
            var entities = new[]
            {
                this.database.CreateSimilarEntities(2, "Name 1", "Description 1"),
                this.database.CreateSimilarEntities(2, "Name 2", "Description 1"),
                this.database.CreateSimilarEntities(2, "Name 2", "Description 2"),
            }
                .SelectMany(e => e)
                .OrderBy(e => e.Description)
                .ThenByDescending(e => e.Name)
                .ThenBy(e => e.Id)
                .ToList();
            var orderByDictionary = new OrderByExpressionDictionary<Entity>()
                .Add(nameof(Entity.Name), e => e.Name)
                .Add(nameof(Entity.Description), e => e.Description)
                .Add(nameof(Entity.Id), e => e.Id);
            this.interpreter
                .Add(new OrderByRequest(nameof(Entity.Description), OrderByDirection.Ascending))
                .Add(new OrderByRequest(nameof(Entity.Name), OrderByDirection.Descending))
                .Add(new OrderByRequest(nameof(Entity.Id), OrderByDirection.Ascending));
            var resultEntities = await Observable.Single(
                    this.database.Create().Entities, this.provider)
                .ApplyOrderByClientRequest(orderByDictionary)
                .ToList();
            Assert.Equal(entities, resultEntities, new PropertyComparer<Entity>());
        }

        private sealed class MockOrderByClientRequestInterpreter : IOrderByClientRequestInterpreter
        {
            private readonly List<OrderByRequest> requestList = new List<OrderByRequest>();

            public IEnumerable<OrderByRequest> ParseRequestQuery(
                ICollection<string> supportedOrderBys) =>
                this.requestList;

            public MockOrderByClientRequestInterpreter Add(OrderByRequest request)
            {
                this.requestList.Add(request);
                return this;
            }
        }
    }
}
