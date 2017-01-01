// <copyright file="MockPrincipal.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Common.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;

    public class MockPrincipal : GenericPrincipal
    {
        private readonly List<Tuple<string, string>> claims = new List<Tuple<string, string>>();

        public MockPrincipal()
            : base(new GenericIdentity(string.Empty), new string[] { })
        {
        }

        public MockPrincipal AddClaim(string type, string value)
        {
            this.claims.Add(Tuple.Create(type, value));
            return this;
        }

        public override bool HasClaim(string type, string value) =>
            this.claims.Any(c => c.Item1 == type && c.Item2 == value);
    }
}
