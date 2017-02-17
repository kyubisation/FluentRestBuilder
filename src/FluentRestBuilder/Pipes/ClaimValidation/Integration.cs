// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Security.Claims;
    using Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Pipes.ClaimValidation;

    public static partial class Integration
    {
        public static IFluentRestBuilderCore RegisterClaimValidationPipe(
            this IFluentRestBuilderCore builder)
        {
            builder.Services.TryAddScoped(
                typeof(IClaimValidationPipeFactory<>),
                typeof(ClaimValidationPipeFactory<>));
            return builder;
        }

        /// <summary>
        /// Validate permissions for the current user.
        /// Results in a forbidden (403) action result on failure.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="predicate">The validation logic.</param>
        /// <param name="errorFactory">A factory function to create the error object.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> CurrentUserHas<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<ClaimsPrincipal, TInput, bool> predicate,
            Func<TInput, object> errorFactory = null)
            where TInput : class =>
            pipe.GetRequiredService<IClaimValidationPipeFactory<TInput>>()
                .Create(predicate, errorFactory, pipe);

        /// <summary>
        /// Validate permissions for the current user.
        /// Results in a forbidden (403) action result on failure.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="predicate">The validation logic.</param>
        /// <param name="error">The error object.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> CurrentUserHas<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<ClaimsPrincipal, TInput, bool> predicate,
            object error)
            where TInput : class =>
            pipe.CurrentUserHas(predicate, i => error);

        /// <summary>
        /// Validate permissions for the current user.
        /// Results in a forbidden (403) action result on failure.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="predicate">The validation logic.</param>
        /// <param name="errorFactory">A factory function to create the error object.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> CurrentUserHas<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<ClaimsPrincipal, bool> predicate,
            Func<TInput, object> errorFactory = null)
            where TInput : class =>
            pipe.CurrentUserHas((p, e) => predicate(p), errorFactory);

        /// <summary>
        /// Validate permissions for the current user.
        /// Results in a forbidden (403) action result on failure.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="predicate">The validation logic.</param>
        /// <param name="error">The error object.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> CurrentUserHas<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<ClaimsPrincipal, bool> predicate,
            object error)
            where TInput : class =>
            pipe.CurrentUserHas((p, e) => predicate(p), error);

        /// <summary>
        /// Validate wether the current user has the given claim.
        /// Results in a forbidden (403) action result on failure.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="claimType">The claim type.</param>
        /// <param name="claim">The required claim.</param>
        /// <param name="errorFactory">A factory function to create the error object.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> CurrentUserHasClaim<TInput>(
            this IOutputPipe<TInput> pipe,
            string claimType,
            string claim,
            Func<TInput, object> errorFactory = null)
            where TInput : class =>
            pipe.CurrentUserHas((p, e) => p.HasClaim(claimType, claim), errorFactory);

        /// <summary>
        /// Validate wether the current user has the given claim.
        /// Results in a forbidden (403) action result on failure.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="claimType">The claim type.</param>
        /// <param name="claim">The required claim.</param>
        /// <param name="error">The error object.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> CurrentUserHasClaim<TInput>(
            this IOutputPipe<TInput> pipe, string claimType, string claim, object error)
            where TInput : class =>
            pipe.CurrentUserHas((p, e) => p.HasClaim(claimType, claim), error);

        /// <summary>
        /// Validate wether the current user has the given claim.
        /// Results in a forbidden (403) action result on failure.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="claimType">The claim type.</param>
        /// <param name="claimFactory">A factory function to create the claim.</param>
        /// <param name="errorFactory">A factory function to create the error object.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> CurrentUserHasClaim<TInput>(
            this IOutputPipe<TInput> pipe,
            string claimType,
            Func<TInput, string> claimFactory,
            Func<TInput, object> errorFactory = null)
            where TInput : class =>
            pipe.CurrentUserHas((p, e) => p.HasClaim(claimType, claimFactory(e)), errorFactory);

        /// <summary>
        /// Validate wether the current user has the given claim.
        /// Results in a forbidden (403) action result on failure.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="claimType">The claim type.</param>
        /// <param name="claimFactory">A factory function to create the claim.</param>
        /// <param name="error">The error object.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> CurrentUserHasClaim<TInput>(
            this IOutputPipe<TInput> pipe,
            string claimType,
            Func<TInput, string> claimFactory,
            object error)
            where TInput : class =>
            pipe.CurrentUserHas((p, e) => p.HasClaim(claimType, claimFactory(e)), error);
    }
}
