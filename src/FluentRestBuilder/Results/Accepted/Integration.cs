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
    using Results.Accepted;

    public static partial class Integration
    {
        public static IFluentRestBuilderCore RegisterAcceptedResult(
            this IFluentRestBuilderCore builder)
        {
            builder.Services.TryAddSingleton(
                typeof(IAcceptedResultFactory<>), typeof(AcceptedResultFactory<>));
            return builder;
        }

        public static Task<IActionResult> ToAcceptedResult<TInput>(
            this IOutputPipe<TInput> pipe)
            where TInput : class =>
            pipe.GetService<IAcceptedResultFactory<TInput>>()
                .Create(pipe)
                .Execute();
    }
}
