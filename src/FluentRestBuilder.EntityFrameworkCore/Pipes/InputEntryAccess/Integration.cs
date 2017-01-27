// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
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

        public static OutputPipe<TInput> ReloadEntity<TInput>(this IOutputPipe<TInput> pipe)
            where TInput : class =>
            pipe.WithEntityEntry(async e => await e.ReloadAsync());

        public static OutputPipe<TInput> LoadReference<TInput, TProperty>(
            this IOutputPipe<TInput> pipe, Expression<Func<TInput, TProperty>> propertyExpression)
            where TInput : class
            where TProperty : class =>
            pipe.WithEntityEntry(async e => await e.Reference(propertyExpression).LoadAsync());

        public static OutputPipe<TInput> LoadCollection<TInput, TProperty>(
            this IOutputPipe<TInput> pipe,
            Expression<Func<TInput, IEnumerable<TProperty>>> propertyExpression)
            where TInput : class
            where TProperty : class =>
            pipe.WithEntityEntry(async e => await e.Collection(propertyExpression).LoadAsync());
    }
}
