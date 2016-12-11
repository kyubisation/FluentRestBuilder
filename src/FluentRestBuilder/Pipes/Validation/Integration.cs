// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Threading.Tasks;
    using FluentRestBuilder;
    using FluentRestBuilder.Pipes.Validation;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;

    public static partial class Integration
    {
        public static ValidationPipe<TInput> InvalidWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<bool> invalidCheck,
            int statusCode,
            object error = null)
            where TInput : class =>
            pipe.GetService<IValidationPipeFactory<TInput>>()
                .Resolve(invalidCheck, statusCode, error, pipe);

        public static ValidationPipe<TInput> InvalidWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<Task<bool>> invalidCheck,
            int statusCode,
            object error = null)
            where TInput : class =>
            pipe.GetService<IValidationPipeFactory<TInput>>()
                .Resolve(invalidCheck, statusCode, error, pipe);

        public static ValidationPipe<TInput> ForbiddenWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<bool> invalidCheck,
            object error = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status403Forbidden, error);

        public static ValidationPipe<TInput> ForbiddenWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<Task<bool>> invalidCheck,
            object error = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status403Forbidden, error);

        public static ValidationPipe<TInput> BadRequestWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<bool> invalidCheck,
            object error = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status400BadRequest, error);

        public static ValidationPipe<TInput> BadRequestWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<Task<bool>> invalidCheck,
            object error = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status400BadRequest, error);

        public static ValidationPipe<TInput> NotFoundWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<bool> invalidCheck,
            object error = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status404NotFound, error);

        public static ValidationPipe<TInput> NotFoundWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<Task<bool>> invalidCheck,
            object error = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status404NotFound, error);
    }
}
