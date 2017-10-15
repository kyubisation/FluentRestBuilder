// <copyright file="AllowedOptionsBuilderExtensions.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Security.Claims;
    using Operators.ActionResult.Options;

    public static class AllowedOptionsBuilderExtensions
    {
        public static AllowedOptionsBuilder<TSource> IsAllowedForAll<TSource>(
                this AllowedOptionsBuilder<TSource> builder,
                Func<ClaimsPrincipal, TSource, bool> validCheck) =>
            builder.IsAllowed(
                new[] { HttpVerb.Delete, HttpVerb.Get, HttpVerb.Patch, HttpVerb.Post, HttpVerb.Put },
                validCheck);

        public static AllowedOptionsBuilder<TSource> IsAllowedForAll<TSource>(
                this AllowedOptionsBuilder<TSource> builder,
                Func<TSource, bool> validCheck) =>
            builder.IsAllowedForAll((p, i) => validCheck(i));

        public static AllowedOptionsBuilder<TSource> IsDeleteAllowed<TSource>(
                this AllowedOptionsBuilder<TSource> builder,
                Func<ClaimsPrincipal, TSource, bool> validCheck) =>
            builder.IsAllowed(new[] { HttpVerb.Delete }, validCheck);

        public static AllowedOptionsBuilder<TSource> IsDeleteAllowed<TSource>(
                this AllowedOptionsBuilder<TSource> builder,
                Func<TSource, bool> validCheck) =>
            builder.IsDeleteAllowed((c, i) => validCheck(i));

        public static AllowedOptionsBuilder<TSource> IsGetAllowed<TSource>(
                this AllowedOptionsBuilder<TSource> builder,
                Func<ClaimsPrincipal, TSource, bool> validCheck) =>
            builder.IsAllowed(new[] { HttpVerb.Get }, validCheck);

        public static AllowedOptionsBuilder<TSource> IsGetAllowed<TSource>(
                this AllowedOptionsBuilder<TSource> builder,
                Func<TSource, bool> validCheck) =>
            builder.IsGetAllowed((c, i) => validCheck(i));

        public static AllowedOptionsBuilder<TSource> IsPatchAllowed<TSource>(
                this AllowedOptionsBuilder<TSource> builder,
                Func<ClaimsPrincipal, TSource, bool> validCheck) =>
            builder.IsAllowed(new[] { HttpVerb.Patch }, validCheck);

        public static AllowedOptionsBuilder<TSource> IsPatchAllowed<TSource>(
                this AllowedOptionsBuilder<TSource> builder,
                Func<TSource, bool> validCheck) =>
            builder.IsPatchAllowed((c, i) => validCheck(i));

        public static AllowedOptionsBuilder<TSource> IsPostAllowed<TSource>(
                this AllowedOptionsBuilder<TSource> builder,
                Func<ClaimsPrincipal, TSource, bool> validCheck) =>
            builder.IsAllowed(new[] { HttpVerb.Post }, validCheck);

        public static AllowedOptionsBuilder<TSource> IsPostAllowed<TSource>(
                this AllowedOptionsBuilder<TSource> builder,
                Func<TSource, bool> validCheck) =>
            builder.IsPostAllowed((c, i) => validCheck(i));

        public static AllowedOptionsBuilder<TSource> IsPutAllowed<TSource>(
                this AllowedOptionsBuilder<TSource> builder,
                Func<ClaimsPrincipal, TSource, bool> validCheck) =>
            builder.IsAllowed(new[] { HttpVerb.Put }, validCheck);

        public static AllowedOptionsBuilder<TSource> IsPutAllowed<TSource>(
                this AllowedOptionsBuilder<TSource> builder,
                Func<TSource, bool> validCheck) =>
            builder.IsPutAllowed((c, i) => validCheck(i));
    }
}
