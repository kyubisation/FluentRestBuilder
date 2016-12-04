// <copyright file="ClaimValidationPipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.Test.ChainPipes.ClaimValidation
{
    using System.Security.Principal;
    using System.Threading.Tasks;
    using Core.ChainPipes.ClaimValidation;
    using Mocks;
    using Xunit;

    public class ClaimValidationPipeTest : TestBaseWithServiceProvider
    {
        [Fact]
        public async Task TestHasClaim()
        {
            var mockPrincipal = new MockPrincipal();
            var resultPipe = MockSourcePipe<Entity>.CreateCompleteChain(
                new Entity(),
                this.ServiceProvider,
                p => new ClaimValidationPipe<Entity>(
                    (u, e) => u.HasClaim(MockPrincipal.ClaimType, MockPrincipal.Claim),
                    mockPrincipal,
                    null,
                    p));
            var result = await resultPipe.Execute();
            Assert.IsType<MockActionResult>(result);
        }

        private class MockPrincipal : GenericPrincipal
        {
            public const string ClaimType = "claimType";
            public const string Claim = "claim";

            public MockPrincipal()
                : base(new GenericIdentity(string.Empty), new string[] { })
            {
            }

            public override bool HasClaim(string type, string value) =>
                type == ClaimType && value == Claim;
        }
    }
}
