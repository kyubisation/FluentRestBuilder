// <copyright file="CurrentUserHasClaimAliases.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators
{
    using System;
    using Filters;

    public static class CurrentUserHasClaimAliases
    {
        /// <summary>
        /// Emits an error if the check fails. Otherwise emits the value.
        /// <para>Requires usage of <see cref="HttpContextProviderAttribute"/>.</para>
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="claimType">The claim type.</param>
        /// <param name="claim">The claim.</param>
        /// <param name="errorFactory">The error factory method.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TFrom}"/>.</returns>
        public static IProviderObservable<TSource> CurrentUserHasClaim<TSource>(
            this IProviderObservable<TSource> observable,
            string claimType,
            string claim,
            Func<TSource, object> errorFactory = null) =>
            observable.CurrentUserHas((p, s) => p.HasClaim(claimType, claim), errorFactory);

        /// <summary>
        /// Emits an error if the check fails. Otherwise emits the value.
        /// <para>Requires usage of <see cref="HttpContextProviderAttribute"/>.</para>
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="claimType">The claim type.</param>
        /// <param name="claim">The claim.</param>
        /// <param name="error">The error to be used on a failed check.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TFrom}"/>.</returns>
        public static IProviderObservable<TSource> CurrentUserHasClaim<TSource>(
            this IProviderObservable<TSource> observable,
            string claimType,
            string claim,
            object error) =>
            observable.CurrentUserHas((p, s) => p.HasClaim(claimType, claim), s => error);

        /// <summary>
        /// Emits an error if the check fails. Otherwise emits the value.
        /// <para>Requires usage of <see cref="HttpContextProviderAttribute"/>.</para>
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="claimType">The claim type.</param>
        /// <param name="claimFactory">The claim factory.</param>
        /// <param name="errorFactory">The error factory method.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TFrom}"/>.</returns>
        public static IProviderObservable<TSource> CurrentUserHasClaim<TSource>(
            this IProviderObservable<TSource> observable,
            string claimType,
            Func<TSource, string> claimFactory,
            Func<TSource, object> errorFactory = null) =>
            observable.CurrentUserHas(
                (p, s) => p.HasClaim(claimType, claimFactory(s)), errorFactory);

        /// <summary>
        /// Emits an error if the check fails. Otherwise emits the value.
        /// <para>Requires usage of <see cref="HttpContextProviderAttribute"/>.</para>
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="claimType">The claim type.</param>
        /// <param name="claimFactory">The claim factory.</param>
        /// <param name="error">The error to be used on a failed check.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TFrom}"/>.</returns>
        public static IProviderObservable<TSource> CurrentUserHasClaim<TSource>(
            this IProviderObservable<TSource> observable,
            string claimType,
            Func<TSource, string> claimFactory,
            object error) =>
            observable.CurrentUserHas(
                (p, s) => p.HasClaim(claimType, claimFactory(s)), s => error);
    }
}
