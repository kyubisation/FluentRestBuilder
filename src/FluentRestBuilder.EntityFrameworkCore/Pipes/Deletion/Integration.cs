// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using EntityFrameworkCore.Builder;
    using EntityFrameworkCore.Pipes.Deletion;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public static partial class Integration
    {
        public static IFluentRestBuilderCoreEntityFrameworkCore RegisterDeletionPipe(
            this IFluentRestBuilderCoreEntityFrameworkCore builder)
        {
            builder.Services.TryAddScoped(
                typeof(IEntityDeletionPipeFactory<>), typeof(EntityDeletionPipeFactory<>));
            return builder;
        }

        /// <summary>
        /// Deletes the received input from the <see cref="DbContext"/>.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> DeleteEntity<TInput>(this IOutputPipe<TInput> pipe)
            where TInput : class =>
            pipe.GetRequiredService<IEntityDeletionPipeFactory<TInput>>()
                .Create(pipe);
    }
}
