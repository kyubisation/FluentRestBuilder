// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Threading.Tasks;
    using Builder;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Sources.LazySource;

    public static partial class Integration
    {
        public static IFluentRestBuilderCore RegisterLazySource(
            this IFluentRestBuilderCore builder)
        {
            builder.Services.TryAddSingleton(
                typeof(ILazySourceFactory<>), typeof(LazySourceFactory<>));
            return builder;
        }

        public static OutputPipe<TOutput> FromSource<TOutput>(
                this ControllerBase controller, Func<Task<TOutput>> output) =>
            controller.HttpContext.RequestServices.GetService<ILazySourceFactory<TOutput>>()
                .Create(output, controller);

        public static OutputPipe<TOutput> FromSource<TOutput>(
            this ControllerBase controller, Func<TOutput> output) =>
            controller.FromSource(() => Task.FromResult(output()));
    }
}
