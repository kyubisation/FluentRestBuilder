// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Threading.Tasks;
    using Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Pipes.Validation;

    public static partial class Integration
    {
        public static IFluentRestBuilderCore RegisterValidationPipe(
            this IFluentRestBuilderCore builder)
        {
            builder.Services.TryAddSingleton(
                typeof(IValidationPipeFactory<>), typeof(ValidationPipeFactory<>));
            return builder;
        }

        public static OutputPipe<TInput> InvalidWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<Task<bool>> invalidCheck,
            int statusCode,
            object error = null)
            where TInput : class =>
            pipe.GetService<IValidationPipeFactory<TInput>>()
                .Create(invalidCheck, statusCode, error, pipe);

        public static OutputPipe<TInput> InvalidWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<bool> invalidCheck,
            int statusCode,
            object error = null)
            where TInput : class =>
            pipe.InvalidWhen(() => Task.FromResult(invalidCheck()), statusCode, error);

        public static OutputPipe<TInput> ForbiddenWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<bool> invalidCheck,
            object error = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status403Forbidden, error);

        public static OutputPipe<TInput> ForbiddenWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<Task<bool>> invalidCheck,
            object error = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status403Forbidden, error);

        public static OutputPipe<TInput> BadRequestWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<bool> invalidCheck,
            object error = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status400BadRequest, error);

        public static OutputPipe<TInput> BadRequestWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<Task<bool>> invalidCheck,
            object error = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status400BadRequest, error);

        public static OutputPipe<TInput> NotFoundWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<bool> invalidCheck,
            object error = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status404NotFound, error);

        public static OutputPipe<TInput> NotFoundWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<Task<bool>> invalidCheck,
            object error = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status404NotFound, error);
    }
}
