// <copyright file="SourceBase.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Sources
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public abstract class SourceBase<TOutput> : OutputPipe<TOutput>
    {
        protected SourceBase(
            ILogger logger,
            IServiceProvider serviceProvider)
            : base(logger, serviceProvider)
        {
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
    }
}
