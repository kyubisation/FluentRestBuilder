// <copyright file="StringifierTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable UnusedAutoPropertyAccessor.Local
namespace FluentRestBuilder.Test
{
    using Xunit;

    public class StringifierTest
    {
        [Fact]
        public void TestConverter()
        {
            const string name = "name";
            const int value = 10;
            var stringValue = new StringifyTest { Name = name, Value = value }.ToString();
            var containedValues = new[]
            {
                nameof(StringifyTest),
                nameof(StringifyTest.Value),
                nameof(StringifyTest.Name),
                name,
                value.ToString(),
            };
            foreach (var containedValue in containedValues)
            {
                Assert.Contains(containedValue, stringValue);
            }
        }

        private sealed class StringifyTest
        {
            public string Name { get; set; }

            public int Value { get; set; }

            public override string ToString() => Stringifier.Convert(this);
        }
    }
}
