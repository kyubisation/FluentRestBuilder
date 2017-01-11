// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System.Threading.Tasks;
    using Builder;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Sources.Source;

    public static partial class Integration
    {
        public static IFluentRestBuilderCore RegisterSource(
            this IFluentRestBuilderCore builder)
        {
            builder.Services.TryAddSingleton(
                typeof(ISourceFactory<>), typeof(SourceFactory<>));
            return builder;
        }

        public static OutputPipe<TOutput> FromSource<TOutput>(
                this ControllerBase controller, Task<TOutput> output) =>
            controller.HttpContext.RequestServices.GetService<ISourceFactory<TOutput>>()
                .Resolve(output, controller);

        public static OutputPipe<TOutput> FromSource<TOutput>(
            this ControllerBase controller, TOutput output) =>
            controller.FromSource(Task.FromResult(output));
    }
}
