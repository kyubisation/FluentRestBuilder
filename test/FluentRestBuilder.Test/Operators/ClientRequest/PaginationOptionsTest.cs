// <copyright file="PaginationOptionsTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Operators.ClientRequest
{
    using System;
    using FluentRestBuilder.Operators.ClientRequest;
    using Xunit;

    public class PaginationOptionsTest
    {
        [Fact]
        public void TestValidConfiguration()
        {
            var options = new PaginationOptions { DefaultLimit = 100, MaxLimit = 1000 };
            options.ThrowOnInvalidConfiguration();
        }

        [Fact]
        public void TestInvalidConfiguration()
        {
            var options = new PaginationOptions { DefaultLimit = 10, MaxLimit = 1 };
            Assert.Throws<InvalidOperationException>(() => options.ThrowOnInvalidConfiguration());
        }
    }
}
