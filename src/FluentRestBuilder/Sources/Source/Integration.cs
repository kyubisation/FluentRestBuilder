// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Sources.Source;

    public static partial class Integration
    {
        public static OutputPipe<TOutput> FromSource<TOutput>(
            this ControllerBase controller, Task<TOutput> output) =>
            new Source<TOutput>(output, controller.HttpContext.RequestServices)
            {
                Controller = controller
            };

        public static OutputPipe<TOutput> FromSource<TOutput>(
            this ControllerBase controller, TOutput output) =>
            controller.FromSource(Task.FromResult(output));
    }
}
