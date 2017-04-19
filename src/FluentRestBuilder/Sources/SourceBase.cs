// <copyright file="SourceBase.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Sources
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Storage;

    public abstract class SourceBase<TOutput> : OutputPipe<TOutput>
    {
        private ControllerBase controller;

        protected SourceBase(
            ILogger logger,
            IServiceProvider serviceProvider)
            : base(logger, serviceProvider)
        {
        }

        public ControllerBase Controller
        {
            get => this.controller;

            set
            {
                this.controller = value;
                this.InitializeControllerServices();
            }
        }

        protected override async Task<IActionResult> Execute()
        {
            Check.IsPipeAttached(this.Child);
            this.Logger.Information?.Log("Retrieving source instance");
            var output = await this.GetOutput();
            this.Logger.Trace?.Log("Using value {0} as source", output);
            return await this.Child.Execute(output);
        }

        protected abstract Task<TOutput> GetOutput();

        private void InitializeControllerServices()
        {
            if (this.Controller == null)
            {
                return;
            }

            var urlHelperStorage = this.GetService<IScopedStorage<IUrlHelper>>();
            if (urlHelperStorage.Value == null)
            {
                urlHelperStorage.Value = this.controller.Url;
            }

            var httpContextStorage = this.GetService<IScopedStorage<HttpContext>>();
            if (httpContextStorage.Value == null)
            {
                httpContextStorage.Value = this.controller.HttpContext;
            }
        }
    }
}
