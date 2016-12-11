// <copyright file="AllowedOptionsBuilderExtensions.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRest
{
    using System;
    using System.Security.Claims;
    using FluentRest.Core;
    using FluentRest.Core.Results.Options;

    public static class AllowedOptionsBuilderExtensions
    {
        public static IAllowedOptionsBuilder<TInput> IsAllowedForAll<TInput>(
            this IAllowedOptionsBuilder<TInput> builder,
            Func<ClaimsPrincipal, TInput, bool> validCheck) =>
            builder.IsAllowed(
                new[] { HttpVerb.Delete, HttpVerb.Get, HttpVerb.Patch, HttpVerb.Post, HttpVerb.Put },
                validCheck);

        public static IAllowedOptionsBuilder<TInput> IsDeleteAllowed<TInput>(
            this IAllowedOptionsBuilder<TInput> builder,
            Func<ClaimsPrincipal, TInput, bool> validCheck) =>
            builder.IsAllowed(new[] { HttpVerb.Delete }, validCheck);

        public static IAllowedOptionsBuilder<TInput> IsGetAllowed<TInput>(
            this IAllowedOptionsBuilder<TInput> builder,
            Func<ClaimsPrincipal, TInput, bool> validCheck) =>
            builder.IsAllowed(new[] { HttpVerb.Get }, validCheck);

        public static IAllowedOptionsBuilder<TInput> IsPatchAllowed<TInput>(
            this IAllowedOptionsBuilder<TInput> builder,
            Func<ClaimsPrincipal, TInput, bool> validCheck) =>
            builder.IsAllowed(new[] { HttpVerb.Patch }, validCheck);

        public static IAllowedOptionsBuilder<TInput> IsPostAllowed<TInput>(
            this IAllowedOptionsBuilder<TInput> builder,
            Func<ClaimsPrincipal, TInput, bool> validCheck) =>
            builder.IsAllowed(new[] { HttpVerb.Post }, validCheck);

        public static IAllowedOptionsBuilder<TInput> IsPutAllowed<TInput>(
            this IAllowedOptionsBuilder<TInput> builder,
            Func<ClaimsPrincipal, TInput, bool> validCheck) =>
            builder.IsAllowed(new[] { HttpVerb.Put }, validCheck);
    }
}
