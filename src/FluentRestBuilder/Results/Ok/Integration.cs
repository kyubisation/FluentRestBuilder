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
    using Results.Ok;

    public static partial class Integration
    {
        public static IFluentRestBuilderCore RegisterOkResult(
            this IFluentRestBuilderCore builder)
        {
            builder.Services.TryAddSingleton(
                typeof(IOkResultFactory<>), typeof(OkResultFactory<>));
            return builder;
        }

        public static Task<IActionResult> ToOkResult<TInput>(
            this IOutputPipe<TInput> pipe)
            where TInput : class =>
            pipe.GetService<IOkResultFactory<TInput>>()
                .Create(pipe)
                .Execute();
    }
}
