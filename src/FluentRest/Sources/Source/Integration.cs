// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRest
{
    using System.Threading.Tasks;
    using Core.Sources.Source;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;

    public static partial class Integration
    {
        public static SourcePipe<TOutput> FromSource<TOutput>(
            this ControllerBase controller, TOutput output) =>
            controller.HttpContext.RequestServices
                .GetService<ISourcePipeFactory<TOutput>>()
                .Resolve(output);

        public static SourcePipe<TOutput> FromSource<TOutput>(
            this ControllerBase controller, Task<TOutput> output) =>
            controller.HttpContext.RequestServices
                .GetService<ISourcePipeFactory<TOutput>>()
                .Resolve(output);
    }
}
