// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using Builder;
    using EntityFrameworkCore.Pipes.Update;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public static partial class Integration
    {
        public static IFluentRestBuilderCore RegisterUpdatePipe(
            this IFluentRestBuilderCore builder)
        {
            builder.Services.TryAddScoped(
                typeof(IEntityUpdatePipeFactory<>), typeof(EntityUpdatePipeFactory<>));
            return builder;
        }

        public static OutputPipe<TInput> UpdateEntity<TInput>(
            this IOutputPipe<TInput> pipe)
            where TInput : class =>
            pipe.GetRequiredService<IEntityUpdatePipeFactory<TInput>>()
                .Create(pipe);
    }
}
