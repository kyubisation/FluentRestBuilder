// <copyright file="WithDbContextAsyncOperator.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Operators;
    using Storage;

    public static class WithDbContextAsyncOperator
    {
        /// <summary>
        /// Perform an async action with the <see cref="DbContext"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="action">The action to be performed.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<TSource> WithDbContextAsync<TSource>(
            this IProviderObservable<TSource> observable, Func<TSource, DbContext, Task> action)
            where TSource : class =>
            new WithDbContextAsyncObservable<TSource>(action, observable);

        private sealed class WithDbContextAsyncObservable<TSource> : Operator<TSource, TSource>
            where TSource : class
        {
            private readonly Func<TSource, DbContext, Task> action;

            public WithDbContextAsyncObservable(
                Func<TSource, DbContext, Task> action,
                IProviderObservable<TSource> observable)
                : base(observable)
            {
                this.action = action;
            }

            protected override IObserver<TSource> Create(
                IObserver<TSource> observer, IDisposable disposable)
            {
                var context = this.ServiceProvider.GetService<IScopedStorage<DbContext>>();
                return new WithDbContextAsyncObserver(
                    this.action, context, observer, disposable);
            }

            private sealed class WithDbContextAsyncObserver : SafeAsyncObserver
            {
                private readonly Func<TSource, DbContext, Task> action;
                private readonly IScopedStorage<DbContext> context;

                public WithDbContextAsyncObserver(
                    Func<TSource, DbContext, Task> action,
                    IScopedStorage<DbContext> context,
                    IObserver<TSource> child,
                    IDisposable disposable)
                    : base(child, disposable)
                {
                    this.action = action;
                    this.context = context;
                }

                protected override async Task<TSource> SafeOnNext(TSource value)
                {
                    await this.action(value, this.context.Value);
                    return value;
                }
            }
        }
    }
}
