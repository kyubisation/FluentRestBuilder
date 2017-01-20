// <copyright file="OptionsResultTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Results.Options
{
    using System;
    using System.Threading.Tasks;
    using Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Primitives;
    using Mocks;
    using Mocks.EntityFramework;
    using Xunit;

    public class OptionsResultTest : IDisposable
    {
        private readonly MockController controller;
        private readonly IServiceProvider provider;

        public OptionsResultTest()
        {
            this.provider = new FluentRestBuilderCore(new ServiceCollection())
                .RegisterSource()
                .RegisterOptionsResultPipe()
                .Services
                .AddSingleton<ILoggerFactory, LoggerFactory>()
                .BuildServiceProvider();
            this.controller = new MockController(this.provider);
        }

        public void Dispose()
        {
            this.controller.Dispose();
        }

        [Fact]
        public async Task TestEmptyCase()
        {
            var result = await this.controller.FromSource(new { })
                .ToOptionsResult();
            var allowedOptions = await this.ExecuteActionResultAndGetAllowHeader(result);
            Assert.Equal("OPTIONS", allowedOptions);
        }

        [Fact]
        public async Task TestGetCase()
        {
            var result = await this.controller.FromSource(new { })
                .ToOptionsResult(HttpVerb.Get);
            var allowedOptions = await this.ExecuteActionResultAndGetAllowHeader(result);
            Assert.Contains("OPTIONS", allowedOptions);
            Assert.Contains("HEAD", allowedOptions);
            Assert.Contains("GET", allowedOptions);
        }

        [Fact]
        public async Task TestBuildingOptions()
        {
            var result = await this.controller.FromSource(new Entity())
                .ToOptionsResult(b => b.IsGetAllowed(i => true));
            var allowedOptions = await this.ExecuteActionResultAndGetAllowHeader(result);
            Assert.Contains("OPTIONS", allowedOptions);
            Assert.Contains("HEAD", allowedOptions);
            Assert.Contains("GET", allowedOptions);
        }

        private async Task<string> ExecuteActionResultAndGetAllowHeader(IActionResult actionResult)
        {
            var actionContext = new ActionContext
            {
                HttpContext = new DefaultHttpContext
                {
                    RequestServices = this.provider
                }
            };
            await actionResult.ExecuteResultAsync(actionContext);
            StringValues allowedOptions;
            var exists = actionContext.HttpContext.Response.Headers
                .TryGetValue("Allow", out allowedOptions);
            Assert.True(exists);

            return allowedOptions.ToString();
        }
    }
}
