// <copyright file="MvcDependencyInjectionTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Xunit;

    public class MvcDependencyInjectionTest
    {
        [Fact]
        public void OptionsTest()
        {
            var provider = new ServiceCollection()
                .AddMvc()
                .Services
                .BuildServiceProvider();
            var options = provider.GetService<IOptions<MvcJsonOptions>>();
            Assert.NotNull(options.Value.SerializerSettings.ContractResolver);
        }
    }
}
