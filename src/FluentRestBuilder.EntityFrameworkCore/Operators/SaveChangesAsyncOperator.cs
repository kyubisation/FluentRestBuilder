// <copyright file="SaveChangesAsyncOperator.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Threading.Tasks;
    using EntityFrameworkCore.Operators;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Operators;
    using Storage;

    public static class SaveChangesAsyncOperator
    {
        /// <summary>
        /// Save changes to the <see cref="DbContext"/> asynchronously.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<TSource> SaveChangesAsync<TSource>(
            this IProviderObservable<TSource> observable)
            where TSource : class =>
            new SaveChangesAsyncObservable<TSource>(observable);

        private sealed class SaveChangesAsyncObservable<TSource> : Operator<TSource, TSource>
            where TSource : class
        {
            public SaveChangesAsyncObservable(IProviderObservable<TSource> observable)
                : base(observable)
            {
            }

            protected override IObserver<TSource> Create(
                IObserver<TSource> observer, IDisposable disposable)
            {
                var context = this.ServiceProvider.GetService<IScopedStorage<DbContext>>();
                return new SaveChangesAsyncObserver(context.Value, observer, disposable);
            }

            private sealed class SaveChangesAsyncObserver : SafeAsyncObserver
            {
                private readonly DbContext context;

                public SaveChangesAsyncObserver(
                    DbContext context, IObserver<TSource> child, IDisposable disposable)
                    : base(child, disposable)
                {
                    this.context = context;
                }

                public override void OnError(Exception error) =>
                    base.OnError(error.ConvertToValidationExceptionIfConcurrencyException());

                protected override async Task<TSource> SafeOnNext(TSource value)
                {
                    await this.context.SaveChangesAsync();
                    return value;
                }
            }
        }
    }
}
