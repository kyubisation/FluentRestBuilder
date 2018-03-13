// <copyright file="CatchOperatorTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Operators
{
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public class CatchOperatorTest
    {
        [Fact]
        public async Task TestCatchWithRethrow()
        {
            var expectedException = new InvalidOperationException();
            InvalidOperationException caughtException = null;
            try
            {
                await Observable.Throw<object>(expectedException)
                    .Catch((InvalidOperationException e) =>
                    {
                        caughtException = e;
                        return Observable.Throw<object>(e);
                    });
            }
            catch (Exception e)
            {
                Assert.Same(expectedException, e);
            }

            Assert.Same(expectedException, caughtException);
        }

        [Fact]
        public async Task TestCatchWithExceptionInHandler()
        {
            var expectedException = new InvalidOperationException();
            try
            {
                await Observable.Throw<object>(new ArgumentException())
                    .Catch((ArgumentException e) => throw expectedException);
            }
            catch (Exception e)
            {
                Assert.Same(expectedException, e);
            }
        }

        [Fact]
        public async Task TestDifferentExceptionPassthrough()
        {
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await Observable
                .Throw<object>(new InvalidOperationException())
                .Catch((ArgumentException e) => Assert.True(false)));
        }

        [Fact]
        public async Task TestCatchWithFallbackValue()
        {
            const string expectedValue = "test";
            var result = await Observable.Throw<string>(new InvalidOperationException())
                .Catch((InvalidOperationException e) => Observable.Single(expectedValue));
            Assert.Equal(expectedValue, result);
        }

        [Fact]
        public async Task TestCatchPassthrough()
        {
            const string expectedValue = "test";
            var result = await Observable.Single(expectedValue)
                .Catch((InvalidOperationException e) => Observable.Single(string.Empty));
            Assert.Equal(expectedValue, result);
        }
    }
}
