// <copyright file="CatchAliasesTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Operators
{
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public class CatchAliasesTest
    {
        [Fact]
        public async Task TestDefaultException()
        {
            Exception exception = null;
            try
            {
                await Observable.Throw<object>(new InvalidOperationException())
                    .Catch(e =>
                    {
                        exception = e;
                        return Observable.Throw<object>(e);
                    });
            }
            catch (Exception)
            {
                // ignored
            }

            Assert.IsType<InvalidOperationException>(exception);
        }

        [Fact]
        public async Task TestAction()
        {
            Exception exception = null;
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await Observable
                .Throw<object>(new InvalidOperationException())
                .Catch(e => exception = e));
            Assert.IsType<InvalidOperationException>(exception);
        }
    }
}
