// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Sources.LazySource;

    public static partial class Integration
    {
        public static OutputPipe<TOutput> FromSource<TOutput>(
            this ControllerBase controller, Func<Task<TOutput>> output) =>
            new LazySource<TOutput>(output, controller.HttpContext.RequestServices)
            {
                Controller = controller
            };

        public static OutputPipe<TOutput> FromSource<TOutput>(
            this ControllerBase controller, Func<TOutput> output) =>
            controller.FromSource(() => Task.FromResult(output()));
    }
}
