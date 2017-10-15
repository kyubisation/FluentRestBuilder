// <copyright file="ApplySearchByClientRequestOperatorTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Operators.ClientRequest
{
    using System.Threading.Tasks;
    using FluentRestBuilder.Operators.ClientRequest.Interpreters;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Mocks.EntityFramework;
    using Xunit;

    public class ApplySearchByClientRequestOperatorTest
    {
        private readonly PersistantDatabase database = new PersistantDatabase();
        private readonly MockSearchByClientRequestInterpreter interpreter =
            new MockSearchByClientRequestInterpreter();

        private readonly ServiceProvider provider;

        public ApplySearchByClientRequestOperatorTest()
        {
            this.provider = new ServiceCollection()
                .AddSingleton<ISearchByClientRequestInterpreter>(this.interpreter)
                .BuildServiceProvider();
        }

        [Fact]
        public async Task TestSimpleSearch()
        {
            this.database.CreateSimilarEntities(3, "Name 1");
            const string expected = "Name 2";
            var entities = this.database.CreateSimilarEntities(3, expected);
            this.database.CreateSimilarEntities(3, "Name 3");
            this.interpreter.SearchString = expected;
            var resultEntities = await Observable.Single(
                    this.database.Create().Entities, this.provider)
                .ApplySearchByClientRequest(f => e => e.Name == f)
                .ToList();
            Assert.Equal(entities, resultEntities, new PropertyComparer<Entity>());
        }

        private sealed class MockSearchByClientRequestInterpreter : ISearchByClientRequestInterpreter
        {
            // ReSharper disable once MemberCanBePrivate.Local
            public string SearchString { get; set; }

            public string ParseRequestQuery() => this.SearchString;
        }
    }
}
