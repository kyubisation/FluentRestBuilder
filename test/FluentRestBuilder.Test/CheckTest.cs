// <copyright file="CheckTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test
{
    using System;
    using Xunit;

    public class CheckTest
    {
        [Fact]
        public void TestIsNullTrue()
        {
            const string paramName = "name";
            Assert.Throws<ArgumentNullException>(paramName, () => Check.IsNull(null, paramName));
        }

        [Fact]
        public void TestIsNullFalse()
        {
            Check.IsNull(new object(), "name");
        }
    }
}
