// <copyright file="SingleObservableTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Observables
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class SingleObservableTest
    {
        [Fact]
        public async Task TestSingle()
        {
            const string expected = "result";
            var single = Observable.Single(expected);
            Assert.Equal(expected, await single);
        }

        [Fact]
        public void TestProvider()
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient<SingleObservableTest>()
                .BuildServiceProvider();
            var single = Observable.Single(string.Empty, serviceProvider);
            var instance = single.ServiceProvider.GetService<SingleObservableTest>();
            Assert.NotNull(instance);
        }
    }
}
