// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Threading.Tasks;
    using FluentRestBuilder.Sources.LazySource;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;

    public static partial class Integration
    {
        public static OutputPipe<TOutput> FromSource<TOutput>(
            this ControllerBase controller, Func<TOutput> output) =>
            controller.HttpContext.RequestServices
                .GetService<ILazySourcePipeFactory<TOutput>>()
                .Resolve(output);

        public static OutputPipe<TOutput> FromSource<TOutput>(
            this ControllerBase controller, Func<Task<TOutput>> output) =>
            controller.HttpContext.RequestServices
                .GetService<ILazySourcePipeFactory<TOutput>>()
                .Resolve(output);
    }
}
