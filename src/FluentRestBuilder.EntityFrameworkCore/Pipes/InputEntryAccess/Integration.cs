// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Threading.Tasks;
    using EntityFrameworkCore.Builder;
    using EntityFrameworkCore.Pipes.InputEntryAccess;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public static partial class Integration
    {
        public static IFluentRestBuilderCoreEntityFrameworkCore RegisterInputEntryAccessPipe(
            this IFluentRestBuilderCoreEntityFrameworkCore builder)
        {
            builder.Services.TryAddScoped(
                typeof(IInputEntryAccessPipeFactory<>), typeof(InputEntryAccessPipeFactory<>));
            return builder;
        }

        public static OutputPipe<TInput> WithEntityEntry<TInput>(
            this IOutputPipe<TInput> pipe, Func<EntityEntry<TInput>, Task> entryAction)
            where TInput : class =>
            pipe.GetRequiredService<IInputEntryAccessPipeFactory<TInput>>()
                .Create(entryAction, pipe);

        public static OutputPipe<TInput> WithEntityEntry<TInput>(
            this IOutputPipe<TInput> pipe, Action<EntityEntry<TInput>> entryAction)
            where TInput : class =>
            pipe.WithEntityEntry(
                e =>
            {
                entryAction(e);
                return Task.FromResult(0);
            });
    }
}
