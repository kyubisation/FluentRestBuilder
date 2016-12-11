// <copyright file="MockPrincipal.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Mocks
{
    using System.Security.Principal;

    public class MockPrincipal : GenericPrincipal
    {
        public MockPrincipal()
            : base(new GenericIdentity(string.Empty), new string[] { })
        {
        }
    }
}
