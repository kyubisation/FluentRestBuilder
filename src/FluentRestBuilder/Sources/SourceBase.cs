// <copyright file="SourceBase.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Sources
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public abstract class SourceBase<TOutput> : OutputPipe<TOutput>
    {
        private ControllerBase controller;
        private Dictionary<Type, object> services;

        protected SourceBase(IServiceProvider serviceProvider)
            : base(serviceProvider)
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
                this.InitializeControllerServices(value);
                this.controller = value;
            }
        }

        protected override object GetService(Type serviceType) =>
            this.TryGetControllerService(serviceType) ?? base.GetService(serviceType);

        protected override async Task<IActionResult> Execute()
        {
            NoPipeAttachedException.Check(this.Child);
            return await this.Child.Execute(await this.GetOutput());
        }

        protected abstract Task<TOutput> GetOutput();

        private void InitializeControllerServices(ControllerBase controllerBase)
        {
            this.services = new Dictionary<Type, object>
            {
                [typeof(IUrlHelper)] = controllerBase.Url,
                [typeof(IHttpContextAccessor)] = new HttpContextAccessor
                {
                    HttpContext = controllerBase.HttpContext
                }
            };
        }

        private object TryGetControllerService(Type serviceType)
        {
            if (this.services == null)
            {
                return null;
            }

            object service;
            return this.services.TryGetValue(serviceType, out service) ? service : null;
        }
    }
}
