// <copyright file="MapToQueryableOperator.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Operators;
    using Storage;

    public static class MapToQueryableOperator
    {
        /// <summary>
        /// Map to a <see cref="IQueryable{TTarget}"/> from the received <see cref="DbContext"/>.
        /// Use the <see cref="DbContext.Set{TEntity}"/> method to select the appropriate
        /// <see cref="IQueryable{TEntity}"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <typeparam name="TTarget">The type of the resulting queryable.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="mapping">The mapping function</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<IQueryable<TTarget>> MapToQueryable<TSource, TTarget>(
            this IProviderObservable<TSource> observable,
            Func<TSource, DbContext, IQueryable<TTarget>> mapping)
            where TSource : class =>
            new MapToQueryableObservable<TSource, TTarget>(mapping, observable);

        private sealed class MapToQueryableObservable<TSource, TTarget> : Operator<TSource, IQueryable<TTarget>>
            where TSource : class
        {
            private readonly Func<TSource, DbContext, IQueryable<TTarget>> mapping;

            public MapToQueryableObservable(
                Func<TSource, DbContext, IQueryable<TTarget>> mapping,
                IProviderObservable<TSource> observable)
                : base(observable)
            {
                this.mapping = mapping;
            }

            protected override IObserver<TSource> Create(
                IObserver<IQueryable<TTarget>> observer, IDisposable disposable)
            {
                var context = this.ServiceProvider.GetService<IScopedStorage<DbContext>>();
                return new MapToQueryableObserver(this.mapping, context.Value, observer, disposable);
            }

            private sealed class MapToQueryableObserver : SafeObserver
            {
                private readonly Func<TSource, DbContext, IQueryable<TTarget>> mapping;
                private readonly DbContext context;

                public MapToQueryableObserver(
                    Func<TSource, DbContext, IQueryable<TTarget>> mapping,
                    DbContext context,
                    IObserver<IQueryable<TTarget>> child,
                    IDisposable disposable)
                    : base(child, disposable)
                {
                    this.mapping = mapping;
                    this.context = context;
                }

                protected override IQueryable<TTarget> SafeOnNext(TSource value) =>
                    this.mapping(value, this.context);
            }
        }
    }
}
