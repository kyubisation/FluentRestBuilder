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
    using Results.NoContent;

    public static partial class Integration
    {
        public static IFluentRestBuilderCore RegisterNoContentResult(
            this IFluentRestBuilderCore builder)
        {
            builder.Services.TryAddSingleton(
                typeof(INoContentResultFactory<>), typeof(NoContentResultFactory<>));
            return builder;
        }

        public static Task<IActionResult> ToNoContentResult<TInput>(
            this IOutputPipe<TInput> pipe)
            where TInput : class
        {
            IPipe resultPipe = pipe.GetService<INoContentResultFactory<TInput>>()
                .Resolve(pipe);
            return resultPipe.Execute();
        }
    }
}
