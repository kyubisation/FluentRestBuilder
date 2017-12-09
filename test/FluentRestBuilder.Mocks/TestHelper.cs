// <copyright file="TestHelper.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Mocks
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class TestHelper
    {
        public static void AssertProvider<TSource>(
            Func<IProviderObservable<TSource>, IProviderObservable<TSource>> operatorFunction) =>
            AssertProvider<TSource, TSource>(operatorFunction);

        public static void AssertProvider<TSource, TTarget>(
            Func<IProviderObservable<TSource>, IProviderObservable<TTarget>> operatorFunction)
        {
            var provider = new ServiceCollection()
                .AddTransient<ProviderToken>()
                .BuildServiceProvider();
            var observable = Observable.Single(default(TSource), provider);
            var result = operatorFunction(observable);
            var token = result.ServiceProvider.GetService<ProviderToken>();
            Assert.NotNull(token);
        }

        // ReSharper disable once ClassNeverInstantiated.Local
        private sealed class ProviderToken
        {
        }
    }
}
