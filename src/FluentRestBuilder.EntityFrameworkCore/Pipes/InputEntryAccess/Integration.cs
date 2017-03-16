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
        public static IFluentRestBuilderEntityFrameworkCoreConfiguration RegisterInputEntryAccessPipe(
            this IFluentRestBuilderEntityFrameworkCoreConfiguration builder)
        {
            builder.Services.TryAddScoped(
                typeof(IInputEntryAccessPipeFactory<>), typeof(InputEntryAccessPipeFactory<>));
            return builder;
        }

        /// <summary>
        /// Perform an asynchronous action with the <see cref="EntityEntry{TInput}"/>
        /// of the received input.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="entryAction">The asynchronous action.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> WithEntityEntry<TInput>(
            this IOutputPipe<TInput> pipe, Func<EntityEntry<TInput>, Task> entryAction)
            where TInput : class
        {
            var factory = pipe.GetService<IInputEntryAccessPipeFactory<TInput>>();
            Check.IsPipeRegistered(factory, typeof(InputEntryAccessPipe<>));
            return factory.Create(entryAction, pipe);
        }

        /// <summary>
        /// Perform an action with the <see cref="EntityEntry{TInput}"/>
        /// of the received input.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="entryAction">The action.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> WithEntityEntry<TInput>(
            this IOutputPipe<TInput> pipe, Action<EntityEntry<TInput>> entryAction)
            where TInput : class =>
            pipe.WithEntityEntry(
                e =>
            {
                entryAction(e);
                return Task.FromResult(0);
            });

        /// <summary>
        /// Reload the received input from the database.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> ReloadEntity<TInput>(this IOutputPipe<TInput> pipe)
            where TInput : class =>
            pipe.WithEntityEntry(async e => await e.ReloadAsync());

        /// <summary>
        /// Load a single reference from the database.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <typeparam name="TProperty">The type of the reference.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="propertyExpression">The property selection expression.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> LoadReference<TInput, TProperty>(
            this IOutputPipe<TInput> pipe, Expression<Func<TInput, TProperty>> propertyExpression)
            where TInput : class
            where TProperty : class =>
            pipe.WithEntityEntry(async e => await e.Reference(propertyExpression).LoadAsync());

        /// <summary>
        /// Load a reference collection from the database.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <typeparam name="TProperty">The type of the reference collection.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="propertyExpression">The property selection expression.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> LoadCollection<TInput, TProperty>(
            this IOutputPipe<TInput> pipe,
            Expression<Func<TInput, IEnumerable<TProperty>>> propertyExpression)
            where TInput : class
            where TProperty : class =>
            pipe.WithEntityEntry(async e => await e.Collection(propertyExpression).LoadAsync());
    }
}
