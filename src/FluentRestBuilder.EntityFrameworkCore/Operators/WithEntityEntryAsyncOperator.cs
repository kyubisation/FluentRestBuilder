// <copyright file="WithEntityEntryAsyncOperator.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.Extensions.DependencyInjection;
    using Operators;
    using Storage;

    public static class WithEntityEntryAsyncOperator
    {
        /// <summary>
        /// Perform an async action with the <see cref="EntityEntry{TEntity}"/>
        /// of the received value.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="action">The action to be performed.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<TSource> WithEntityEntryAsync<TSource>(
            this IProviderObservable<TSource> observable, Func<EntityEntry<TSource>, Task> action)
            where TSource : class =>
            new WithEntityEntryAsyncObservable<TSource>(action, observable);

        private sealed class WithEntityEntryAsyncObservable<TSource> : Operator<TSource, TSource>
            where TSource : class
        {
            private readonly Func<EntityEntry<TSource>, Task> action;

            public WithEntityEntryAsyncObservable(
                Func<EntityEntry<TSource>, Task> action,
                IProviderObservable<TSource> observable)
                : base(observable)
            {
                this.action = action;
            }

            protected override IObserver<TSource> Create(
                IObserver<TSource> observer, IDisposable disposable)
            {
                var context = this.ServiceProvider.GetService<IScopedStorage<DbContext>>();
                return new WithEntityEntryAsyncObserver(
                    this.action, context.Value, observer, disposable);
            }

            private sealed class WithEntityEntryAsyncObserver : SafeAsyncObserver
            {
                private readonly Func<EntityEntry<TSource>, Task> action;
                private readonly DbContext context;

                public WithEntityEntryAsyncObserver(
                    Func<EntityEntry<TSource>, Task> action,
                    DbContext context,
                    IObserver<TSource> child,
                    IDisposable disposable)
                    : base(child, disposable)
                {
                    this.action = action;
                    this.context = context;
                }

                protected override async Task<TSource> SafeOnNext(TSource value)
                {
                    var entityEntry = this.context.Entry(value);
                    await this.action(entityEntry);
                    return value;
                }
            }
        }
    }
}
