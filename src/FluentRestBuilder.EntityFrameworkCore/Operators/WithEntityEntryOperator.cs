// <copyright file="WithEntityEntryOperator.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.Extensions.DependencyInjection;
    using Operators;
    using Storage;

    public static class WithEntityEntryOperator
    {
        /// <summary>
        /// Perform an action with the <see cref="EntityEntry{TEntity}"/> of the received value.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="action">The action to be performed.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<TSource> WithEntityEntry<TSource>(
            this IProviderObservable<TSource> observable, Action<EntityEntry<TSource>> action)
            where TSource : class =>
            new WithEntityEntryObservable<TSource>(action, observable);

        private sealed class WithEntityEntryObservable<TSource> : Operator<TSource, TSource>
            where TSource : class
        {
            private readonly Action<EntityEntry<TSource>> action;

            public WithEntityEntryObservable(
                Action<EntityEntry<TSource>> action,
                IProviderObservable<TSource> observable)
                : base(observable)
            {
                this.action = action;
            }

            protected override IObserver<TSource> Create(
                IObserver<TSource> observer, IDisposable disposable)
            {
                var context = this.ServiceProvider.GetService<IScopedStorage<DbContext>>();
                return new WithEntityEntryObserver(
                    this.action, context.Value, observer, disposable);
            }

            private sealed class WithEntityEntryObserver : SafeObserver
            {
                private readonly Action<EntityEntry<TSource>> action;
                private readonly DbContext context;

                public WithEntityEntryObserver(
                    Action<EntityEntry<TSource>> action,
                    DbContext context,
                    IObserver<TSource> child,
                    IDisposable disposable)
                    : base(child, disposable)
                {
                    this.action = action;
                    this.context = context;
                }

                protected override TSource SafeOnNext(TSource value)
                {
                    var entityEntry = this.context.Entry(value);
                    this.action(entityEntry);
                    return value;
                }
            }
        }
    }
}
