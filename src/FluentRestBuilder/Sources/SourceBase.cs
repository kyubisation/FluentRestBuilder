// <copyright file="SourceBase.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Sources
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public abstract class SourceBase<TOutput> : OutputPipe<TOutput>
    {
        protected SourceBase(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        public IUrlHelper UrlHelper { get; set; }

        protected override object GetService(Type serviceType)
        {
            if (serviceType == typeof(IUrlHelper) && this.UrlHelper != null)
            {
                return this.UrlHelper;
            }

            return base.GetService(serviceType);
        }

        protected override async Task<IActionResult> Execute()
        {
            NoPipeAttachedException.Check(this.Child);
            return await this.Child.Execute(await this.GetOutput());
        }

        protected abstract Task<TOutput> GetOutput();
    }
}
