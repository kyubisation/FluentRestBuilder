// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using Builder;
    using EntityFrameworkCore.Pipes.Reload;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public static partial class Integration
    {
        internal static IFluentRestBuilder RegisterReloadPipe(
            this IFluentRestBuilder builder)
        {
            builder.Services.TryAddScoped(
                typeof(IReloadPipeFactory<>), typeof(ReloadPipeFactory<>));
            return builder;
        }

        public static OutputPipe<TInput> ReloadEntity<TInput>(
            this IOutputPipe<TInput> pipe)
            where TInput : class =>
            pipe.GetRequiredService<IReloadPipeFactory<TInput>>()
                .Resolve(pipe);
    }
}
