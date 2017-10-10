// <copyright file="OptionsResultTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Operators.ActionResult.Options
{
    using FluentRestBuilder.Operators.ActionResult.Options;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class OptionsResultTest
    {
        [Fact]
        public void TestAssignment()
        {
            var provider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();
            var result = new OptionsResult(
                new[]
                {
                    HttpVerb.Delete,
                    HttpVerb.Get,
                    HttpVerb.Patch,
                    HttpVerb.Post,
                    HttpVerb.Put,
                });
            var httpContext = new DefaultHttpContext { RequestServices = provider };
            var actionContext = new ActionContext { HttpContext = httpContext };
            result.ExecuteResult(actionContext);
            Assert.Contains("Allow", httpContext.Response.Headers.Keys);
            foreach (var verb in new[] { "HEAD", "GET", "DELETE", "PATCH", "POST", "PUT" })
            {
                Assert.Contains(verb, httpContext.Response.Headers["Allow"].ToString());
            }
        }
    }
}
