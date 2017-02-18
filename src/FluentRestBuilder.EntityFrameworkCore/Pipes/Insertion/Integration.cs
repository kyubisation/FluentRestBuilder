// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using EntityFrameworkCore.Builder;
    using EntityFrameworkCore.Pipes.Insertion;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public static partial class Integration
    {
        public static IFluentRestBuilderCoreEntityFrameworkCore RegisterInsertionPipe(
            this IFluentRestBuilderCoreEntityFrameworkCore builder)
        {
            builder.Services.TryAddScoped(
                typeof(IEntityInsertionPipeFactory<>), typeof(EntityInsertionPipeFactory<>));
            return builder;
        }

        /// <summary>
        /// Inserts the input into the database.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> InsertEntity<TInput>(
            this IOutputPipe<TInput> pipe)
            where TInput : class =>
            pipe.GetRequiredService<IEntityInsertionPipeFactory<TInput>>()
                .Create(pipe);
    }
}
