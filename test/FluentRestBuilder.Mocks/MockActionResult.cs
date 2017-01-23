// <copyright file="MockActionResult.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Mocks
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public class MockActionResult : ObjectResult
    {
        public MockActionResult(object value)
            : base(value)
        {
        }

        public override Task ExecuteResultAsync(ActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}