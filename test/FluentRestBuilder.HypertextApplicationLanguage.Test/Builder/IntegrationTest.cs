// <copyright file="IntegrationTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage.Test.Builder
{
    using FluentRestBuilder.Builder;
    using HypertextApplicationLanguage.Mapping;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Xunit;

    public class IntegrationTest
    {
        [Fact]
        public void TestAddingRestMapper()
        {
            const string embed1 = "test";
            const string embed2 = "test1";
            var provider = new FluentRestBuilderCoreConfiguration(new ServiceCollection())
                .AddRestMapper(p => RestMapper<Target, TargetResponse>.Create(
                    p,
                    target => new TargetResponse
                    {
                        Id = target.Id,
                        Id2 = target.Id2,
                        Id3 = target.Id3,
                        Id4 = target.Id4,
                    }).Embed(embed1, embed1))
                .Services
                .BuildServiceProvider();
            using (var scope = provider.CreateScope())
            {
                var mapper = scope.ServiceProvider
                    .GetService<IMapper<Target, TargetResponse>>();
                Assert.IsType<RestMapper<Target, TargetResponse>>(mapper);
                var result = mapper
                    .Embed(embed2, embed2)
                    .Map(new Target());
                Assert.True(result.Embedded.ContainsKey(embed1));
                Assert.True(result.Embedded.ContainsKey(embed2));
                Assert.Equal(embed1, result.Embedded[embed1]);
                Assert.Equal(embed2, result.Embedded[embed2]);
            }
        }
    }
}
