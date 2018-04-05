// <copyright file="WithDbContextOperator.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Operators;
    using Storage;

    public static class WithDbContextOperator
    {
        /// <summary>
        /// Perform an action with the <see cref="DbContext"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="action">The action to be performed.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<TSource> WithDbContext<TSource>(
            this IProviderObservable<TSource> observable, Action<TSource, DbContext> action)
            where TSource : class =>
            new WithDbContextObservable<TSource>(action, observable);

        private sealed class WithDbContextObservable<TSource> : Operator<TSource, TSource>
            where TSource : class
        {
            private readonly Action<TSource, DbContext> action;

            public WithDbContextObservable(
                Action<TSource, DbContext> action,
                IProviderObservable<TSource> observable)
                : base(observable)
            {
                this.action = action;
            }

            protected override IObserver<TSource> Create(
                IObserver<TSource> observer, IDisposable disposable)
            {
                var context = this.ServiceProvider.GetService<IScopedStorage<DbContext>>();
                return new WithDbContextObserver(this.action, context, observer, disposable);
            }

            private sealed class WithDbContextObserver : SafeObserver
            {
                private readonly Action<TSource, DbContext> action;
                private readonly IScopedStorage<DbContext> context;

                public WithDbContextObserver(
                    Action<TSource, DbContext> action,
                    IScopedStorage<DbContext> context,
                    IObserver<TSource> child,
                    IDisposable disposable)
                    : base(child, disposable)
                {
                    this.action = action;
                    this.context = context;
                }

                protected override TSource SafeOnNext(TSource value)
                {
                    this.action(value, this.context.Value);
                    return value;
                }
            }
        }
    }
}
