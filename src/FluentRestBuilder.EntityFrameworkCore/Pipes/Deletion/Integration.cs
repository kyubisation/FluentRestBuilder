// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using Builder;
    using EntityFrameworkCore.Pipes.Deletion;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public static partial class Integration
    {
        internal static IFluentRestBuilder RegisterDeletionPipe(
            this IFluentRestBuilder builder)
        {
            builder.Services.TryAddScoped(
                typeof(IEntityDeletionPipeFactory<>), typeof(EntityDeletionPipeFactory<>));
            return builder;
        }

        public static OutputPipe<TInput> DeleteEntity<TInput>(this IOutputPipe<TInput> pipe)
            where TInput : class =>
            pipe.GetRequiredService<IEntityDeletionPipeFactory<TInput>>()
                .Resolve(pipe);
    }
}
