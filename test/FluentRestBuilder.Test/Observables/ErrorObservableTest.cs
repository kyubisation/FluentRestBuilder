// <copyright file="ErrorObservableTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Observables
{
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public class ErrorObservableTest
    {
        [Fact]
        public async Task TestErrorObservable()
        {
            var exception = new InvalidOperationException();
            try
            {
                await Observable.Throw<ErrorObservableTest>(exception);
            }
            catch (Exception e)
            {
                Assert.Same(exception, e);
            }
        }
    }
}
