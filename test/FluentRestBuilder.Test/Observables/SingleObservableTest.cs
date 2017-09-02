// <copyright file="SingleObservableTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Observables
{
    using System.Threading.Tasks;
    using FluentRestBuilder;
    using FluentRestBuilder.Observables;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class SingleObservableTest
    {
        [Fact]
        public async Task TestSingle()
        {
            const string expected = "result";
            var single = new SingleObservable<string>(
                expected, new ServiceCollection().BuildServiceProvider());
            Assert.Equal(expected, await single);
        }

        [Fact]
        public void TestProvider()
        {
            var collection = new ServiceCollection();
            collection.AddTransient<SingleObservableTest>();
            var single = new SingleObservable<string>(
                string.Empty, collection.BuildServiceProvider());
            var instance = single.ServiceProvider.GetService<SingleObservableTest>();
            Assert.NotNull(instance);
        }
    }
}
