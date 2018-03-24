// <copyright file="DeleteEntityOperator.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using EntityFrameworkCore.Operators.Exceptions;
    using Microsoft.EntityFrameworkCore;

    public static class DeleteEntityOperator
    {
        /// <summary>
        /// Remove the received entity from the <see cref="DbContext"/> and save the change.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<TSource> DeleteEntity<TSource>(
            this IProviderObservable<TSource> observable)
            where TSource : class =>
            observable.WithDbContextAsync(async (source, context) =>
                {
                    context.Remove(source);
                    await context.SaveChangesAsync();
                })
                .Catch((DbUpdateConcurrencyException exception) =>
                    Observable.Throw<TSource>(
                        new ConflictException(exception), observable.ServiceProvider));
    }
}
