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
    using Storage;

    public abstract class SourceBase<TOutput> : OutputPipe<TOutput>
    {
        private ControllerBase controller;

        protected SourceBase(IServiceProvider serviceProvider)
            : base(null, serviceProvider)
        {
        }

        public ControllerBase Controller
        {
            get
            {
                return this.controller;
            }

            set
            {
                this.controller = value;
                this.InitializeControllerServices();
            }
        }

        protected override async Task<IActionResult> Execute()
        {
            Check.IsPipeAttached(this.Child);
            return await this.Child.Execute(await this.GetOutput());
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
