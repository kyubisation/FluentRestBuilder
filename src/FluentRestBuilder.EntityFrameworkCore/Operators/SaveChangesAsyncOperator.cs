// <copyright file="SaveChangesAsyncOperator.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using EntityFrameworkCore.Operators.Exceptions;
    using Microsoft.EntityFrameworkCore;

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
            observable.WithDbContextAsync(async (source, context) => await context.SaveChangesAsync())
                .Catch((DbUpdateConcurrencyException exception) =>
                    Observable.Throw<TSource>(
                        new ConflictException(exception), observable.ServiceProvider));
    }
}
