// <copyright file="ClaimValidationPipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Pipes.ClaimValidation
{
    using System.Threading.Tasks;
    using Common.Mocks;
    using FluentRestBuilder.Pipes.ClaimValidation;
    using FluentRestBuilder.Sources.Source;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class ClaimValidationPipeTest : TestBaseWithServiceProvider
    {
        public const string ClaimType = "claimType";
        public const string Claim = "claim";
        private readonly Source<Entity> source;
        private readonly MockPrincipal principal;

        public ClaimValidationPipeTest()
        {
            this.principal = new MockPrincipal();
            var provider = new ServiceCollection()
                .AddSingleton<IHttpContextAccessor>(p => new HttpContextAccessor
                {
                    HttpContext = new DefaultHttpContext { User = this.principal }
                })
                .AddTransient<IClaimValidationPipeFactory<Entity>, ClaimValidationPipeFactory<Entity>>()
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
