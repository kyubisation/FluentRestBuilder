// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRest
{
    using System;
    using System.Threading.Tasks;
    using Core;
    using Core.ChainPipes.EntityValidation;
    using Microsoft.AspNetCore.Http;

    public static partial class Integration
    {
        public static EntityValidationPipe<TEntity> InvalidWhen<TEntity>(
            this IOutputPipe<TEntity> pipe,
            Func<TEntity, bool> invalidCheck,
            int statusCode,
            object error = null)
            where TEntity : class =>
            new EntityValidationPipe<TEntity>(invalidCheck, statusCode, error, pipe);

        public static EntityValidationPipe<TEntity> InvalidWhen<TEntity>(
            this IOutputPipe<TEntity> pipe,
            Func<TEntity, Task<bool>> invalidCheck,
            int statusCode,
            object error = null)
            where TEntity : class =>
            new EntityValidationPipe<TEntity>(invalidCheck, statusCode, error, pipe);

        public static EntityValidationPipe<TEntity> ForbiddenWhen<TEntity>(
            this IOutputPipe<TEntity> pipe,
            Func<TEntity, bool> invalidCheck,
            object error = null)
            where TEntity : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status403Forbidden, error);

        public static EntityValidationPipe<TEntity> ForbiddenWhen<TEntity>(
            this IOutputPipe<TEntity> pipe,
            Func<TEntity, Task<bool>> invalidCheck,
            object error = null)
            where TEntity : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status403Forbidden, error);

        public static EntityValidationPipe<TEntity> BadRequestWhen<TEntity>(
            this IOutputPipe<TEntity> pipe,
            Func<TEntity, bool> invalidCheck,
            object error = null)
            where TEntity : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status400BadRequest, error);

        public static EntityValidationPipe<TEntity> BadRequestWhen<TEntity>(
            this IOutputPipe<TEntity> pipe,
            Func<TEntity, Task<bool>> invalidCheck,
            object error = null)
            where TEntity : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status400BadRequest, error);

        public static EntityValidationPipe<TEntity> NotFoundWhenEmpty<TEntity>(
            this IOutputPipe<TEntity> pipe, object error = null)
            where TEntity : class =>
            InvalidWhen<TEntity>(
                pipe, e => e == null, StatusCodes.Status404NotFound, error);

        public static EntityValidationPipe<TEntity> NotFoundWhen<TEntity>(
            this IOutputPipe<TEntity> pipe,
            Func<TEntity, bool> invalidCheck,
            object error = null)
            where TEntity : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status404NotFound, error);

        public static EntityValidationPipe<TEntity> NotFoundWhen<TEntity>(
            this IOutputPipe<TEntity> pipe,
            Func<TEntity, Task<bool>> invalidCheck,
            object error = null)
            where TEntity : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status404NotFound, error);

        public static EntityValidationPipe<TEntity> GoneWhen<TEntity>(
            this IOutputPipe<TEntity> pipe,
            Func<TEntity, bool> invalidCheck,
            object error = null)
            where TEntity : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status410Gone, error);

        public static EntityValidationPipe<TEntity> GoneWhen<TEntity>(
            this IOutputPipe<TEntity> pipe,
            Func<TEntity, Task<bool>> invalidCheck,
            object error = null)
            where TEntity : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status410Gone, error);
    }
}
