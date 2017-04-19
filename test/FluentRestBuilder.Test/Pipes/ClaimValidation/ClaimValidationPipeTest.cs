// <copyright file="ClaimValidationPipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Pipes.ClaimValidation
{
    using System.Threading.Tasks;
    using Builder;
    using FluentRestBuilder.Sources.Source;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Mocks.EntityFramework;
    using Storage;
    using Xunit;

    public class ClaimValidationPipeTest
    {
        public const string ClaimType = "claimType";
        public const string Claim = "claim";
        private readonly Source<Entity> source;
        private readonly MockPrincipal principal;

        public ClaimValidationPipeTest()
        {
            this.principal = new MockPrincipal();
            var provider = new FluentRestBuilderCoreConfiguration(new ServiceCollection())
                .RegisterClaimValidationPipe()
                .Services
                .AddSingleton<IScopedStorage<HttpContext>>(p => new ScopedStorage<HttpContext>
                {
                    Value = new DefaultHttpContext { User = this.principal },
                })
                .BuildServiceProvider();
            this.source = new Source<Entity>(new Entity(), provider);
        }

        [Fact]
        public async Task TestHasClaim()
        {
            this.principal.AddClaim(ClaimType, Claim);
            var result = await this.source
                .CurrentUserHasClaim(ClaimType, Claim)
                .ToObjectResultOrDefault();
            Assert.NotNull(result);
        }

        [Fact]
        public async Task TestMissingClaim()
        {
            var result = await this.source
                .CurrentUserHasClaim(ClaimType, Claim)
                .ToMockResultPipe()
                .Execute();
            Assert.IsAssignableFrom<StatusCodeResult>(result);
            Assert.Equal(StatusCodes.Status403Forbidden, ((StatusCodeResult)result).StatusCode);
            Assert.Null(result.GetObjectResultOrDefault<Entity>());
        }
    }
}
