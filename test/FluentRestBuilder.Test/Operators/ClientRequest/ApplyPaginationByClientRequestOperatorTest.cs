// <copyright file="ApplyPaginationByClientRequestOperatorTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Operators.ClientRequest
{
    using System.Linq;
    using System.Threading.Tasks;
    using FluentRestBuilder.Operators.ClientRequest;
    using FluentRestBuilder.Operators.ClientRequest.Interpreters;
    using FluentRestBuilder.Operators.ClientRequest.Interpreters.Requests;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Mocks.EntityFramework;
    using Xunit;

    public class ApplyPaginationByClientRequestOperatorTest
    {
        private readonly PersistantDatabase database = new PersistantDatabase();
        private readonly MockPaginationByClientRequestInterpreter interpreter =
            new MockPaginationByClientRequestInterpreter();

        private readonly ServiceProvider provider;

        public ApplyPaginationByClientRequestOperatorTest()
        {
            this.provider = new ServiceCollection()
                .AddFluentRestBuilder()
                .Services
                .AddSingleton<IPaginationByClientRequestInterpreter>(this.interpreter)
                .BuildServiceProvider();
        }

        [Fact]
        public async Task TestSimpleUseCase()
        {
            var options = new PaginationOptions();
            var entities = this.database.CreateEnumeratedEntities(50)
                .Take(options.DefaultLimit)
                .ToList();
            var resultEntities = await Observable.Single(
                    this.database.Create().Entities, this.provider)
                .ApplyPaginationByClientRequest()
                .ToList();
            Assert.Equal(entities, resultEntities, new PropertyComparer<Entity>());
        }

        [Fact]
        public async Task TestWithOptions()
        {
            var options = new PaginationOptions { DefaultLimit = 10 };
            var entities = this.database.CreateEnumeratedEntities(50)
                .Take(options.DefaultLimit)
                .ToList();
            var resultEntities = await Observable.Single(
                    this.database.Create().Entities, this.provider)
                .ApplyPaginationByClientRequest(options)
                .ToList();
            Assert.Equal(entities, resultEntities, new PropertyComparer<Entity>());
        }

        [Fact]
        public async Task TestMaxLimit()
        {
            var options = new PaginationOptions { DefaultLimit = 10, MaxLimit = 10 };
            var entities = this.database.CreateEnumeratedEntities(50)
                .Take(options.DefaultLimit)
                .ToList();
            this.interpreter.Request = new PaginationRequest(0, 20);
            var resultEntities = await Observable.Single(
                    this.database.Create().Entities, this.provider)
                .ApplyPaginationByClientRequest(options)
                .ToList();
            Assert.Equal(entities, resultEntities, new PropertyComparer<Entity>());
        }

        [Fact]
        public async Task TestWithParameters()
        {
            var entities = this.database.CreateEnumeratedEntities(10)
                .Skip(2)
                .Take(2)
                .ToList();
            this.interpreter.Request = new PaginationRequest(2, 2);
            var resultEntities = await Observable.Single(
                    this.database.Create().Entities, this.provider)
                .ApplyPaginationByClientRequest()
                .ToList();
            Assert.Equal(entities, resultEntities, new PropertyComparer<Entity>());
        }

        private sealed class MockPaginationByClientRequestInterpreter : IPaginationByClientRequestInterpreter
        {
            // ReSharper disable once MemberCanBePrivate.Local
            public PaginationRequest Request { get; set; } = new PaginationRequest();

            public PaginationRequest ParseRequestQuery() => this.Request;
        }
    }
}
