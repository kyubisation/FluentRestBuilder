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
    using Pipes.EntityValidation;

    public static partial class Integration
    {
        public static IFluentRestBuilderCore RegisterEntityValidationPipe(
            this IFluentRestBuilderCore builder)
        {
            builder.Services.TryAddSingleton(
                typeof(IEntityValidationPipeFactory<>),
                typeof(EntityValidationPipeFactory<>));
            return builder;
        }

        public static OutputPipe<TInput> InvalidWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<TInput, Task<bool>> invalidCheck,
            int statusCode,
            Func<TInput, object> errorFactory = null)
            where TInput : class =>
            pipe.GetService<IEntityValidationPipeFactory<TInput>>()
                .Create(invalidCheck, statusCode, errorFactory, pipe);

        public static OutputPipe<TInput> InvalidWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<TInput, Task<bool>> invalidCheck,
            int statusCode,
            object error)
            where TInput : class =>
            pipe.InvalidWhen(invalidCheck, statusCode, i => error);

        public static OutputPipe<TInput> InvalidWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<TInput, bool> invalidCheck,
            int statusCode,
            Func<TInput, object> errorFactory = null)
            where TInput : class =>
            pipe.InvalidWhen(e => Task.FromResult(invalidCheck(e)), statusCode, errorFactory);

        public static OutputPipe<TInput> InvalidWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<TInput, bool> invalidCheck,
            int statusCode,
            object error)
            where TInput : class =>
            pipe.InvalidWhen(e => Task.FromResult(invalidCheck(e)), statusCode, error);

        public static OutputPipe<TInput> InvalidWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<Task<bool>> invalidCheck,
            int statusCode,
            Func<TInput, object> errorFactory = null)
            where TInput : class =>
            pipe.InvalidWhen(e => invalidCheck(), statusCode, errorFactory);

        public static OutputPipe<TInput> InvalidWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<Task<bool>> invalidCheck,
            int statusCode,
            object error)
            where TInput : class =>
            pipe.InvalidWhen(e => invalidCheck(), statusCode, error);

        public static OutputPipe<TInput> InvalidWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<bool> invalidCheck,
            int statusCode,
            Func<TInput, object> errorFactory = null)
            where TInput : class =>
            pipe.InvalidWhen(() => Task.FromResult(invalidCheck()), statusCode, errorFactory);

        public static OutputPipe<TInput> InvalidWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<bool> invalidCheck,
            int statusCode,
            object error)
            where TInput : class =>
            pipe.InvalidWhen(() => Task.FromResult(invalidCheck()), statusCode, error);

        public static OutputPipe<TInput> ForbiddenWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<TInput, bool> invalidCheck,
            Func<TInput, object> errorFactory = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status403Forbidden, errorFactory);

        public static OutputPipe<TInput> ForbiddenWhen<TInput>(
            this IOutputPipe<TInput> pipe, Func<TInput, bool> invalidCheck, object error)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status403Forbidden, error);

        public static OutputPipe<TInput> ForbiddenWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<TInput, Task<bool>> invalidCheck,
            Func<TInput, object> errorFactory = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status403Forbidden, errorFactory);

        public static OutputPipe<TInput> ForbiddenWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<TInput, Task<bool>> invalidCheck,
            object error)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status403Forbidden, error);

        public static OutputPipe<TInput> ForbiddenWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<bool> invalidCheck,
            Func<TInput, object> errorFactory = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status403Forbidden, errorFactory);

        public static OutputPipe<TInput> ForbiddenWhen<TInput>(
            this IOutputPipe<TInput> pipe, Func<bool> invalidCheck, object error)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status403Forbidden, error);

        public static OutputPipe<TInput> ForbiddenWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<Task<bool>> invalidCheck,
            Func<TInput, object> errorFactory = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status403Forbidden, errorFactory);

        public static OutputPipe<TInput> ForbiddenWhen<TInput>(
            this IOutputPipe<TInput> pipe, Func<Task<bool>> invalidCheck, object error)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status403Forbidden, error);

        public static OutputPipe<TInput> BadRequestWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<TInput, bool> invalidCheck,
            Func<TInput, object> errorFactory = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status400BadRequest, errorFactory);

        public static OutputPipe<TInput> BadRequestWhen<TInput>(
            this IOutputPipe<TInput> pipe, Func<TInput, bool> invalidCheck, object error)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status400BadRequest, error);

        public static OutputPipe<TInput> BadRequestWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<TInput, Task<bool>> invalidCheck,
            Func<TInput, object> errorFactory = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status400BadRequest, errorFactory);

        public static OutputPipe<TInput> BadRequestWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<TInput, Task<bool>> invalidCheck,
            object error)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status400BadRequest, error);

        public static OutputPipe<TInput> BadRequestWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<bool> invalidCheck,
            Func<TInput, object> errorFactory = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status400BadRequest, errorFactory);

        public static OutputPipe<TInput> BadRequestWhen<TInput>(
            this IOutputPipe<TInput> pipe, Func<bool> invalidCheck, object error)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status400BadRequest, error);

        public static OutputPipe<TInput> BadRequestWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<Task<bool>> invalidCheck,
            Func<TInput, object> errorFactory = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status400BadRequest, errorFactory);

        public static OutputPipe<TInput> BadRequestWhen<TInput>(
            this IOutputPipe<TInput> pipe, Func<Task<bool>> invalidCheck, object error)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status400BadRequest, error);

        public static OutputPipe<TInput> NotFoundWhenEmpty<TInput>(
            this IOutputPipe<TInput> pipe, object error = null)
            where TInput : class =>
            InvalidWhen(pipe, e => e == null, StatusCodes.Status404NotFound, error);

        public static OutputPipe<TInput> NotFoundWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<TInput, bool> invalidCheck,
            Func<TInput, object> errorFactory = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status404NotFound, errorFactory);

        public static OutputPipe<TInput> NotFoundWhen<TInput>(
            this IOutputPipe<TInput> pipe, Func<TInput, bool> invalidCheck, object error)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status404NotFound, error);

        public static OutputPipe<TInput> NotFoundWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<TInput, Task<bool>> invalidCheck,
            Func<TInput, object> errorFactory = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status404NotFound, errorFactory);

        public static OutputPipe<TInput> NotFoundWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<TInput, Task<bool>> invalidCheck,
            object error)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status404NotFound, error);

        public static OutputPipe<TInput> NotFoundWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<bool> invalidCheck,
            Func<TInput, object> errorFactory = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status404NotFound, errorFactory);

        public static OutputPipe<TInput> NotFoundWhen<TInput>(
            this IOutputPipe<TInput> pipe, Func<bool> invalidCheck, object error)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status404NotFound, error);

        public static OutputPipe<TInput> NotFoundWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<Task<bool>> invalidCheck,
            Func<TInput, object> errorFactory = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status404NotFound, errorFactory);

        public static OutputPipe<TInput> NotFoundWhen<TInput>(
            this IOutputPipe<TInput> pipe, Func<Task<bool>> invalidCheck, object error)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status404NotFound, error);

        public static OutputPipe<TInput> GoneWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<TInput, bool> invalidCheck,
            Func<TInput, object> errorFactory = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status410Gone, errorFactory);

        public static OutputPipe<TInput> GoneWhen<TInput>(
            this IOutputPipe<TInput> pipe, Func<TInput, bool> invalidCheck, object error)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status410Gone, error);

        public static OutputPipe<TInput> GoneWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<TInput, Task<bool>> invalidCheck,
            Func<TInput, object> errorFactory = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status410Gone, errorFactory);

        public static OutputPipe<TInput> GoneWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<TInput, Task<bool>> invalidCheck,
            object error)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status410Gone, error);

        public static OutputPipe<TInput> GoneWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<bool> invalidCheck,
            Func<TInput, object> errorFactory = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status410Gone, errorFactory);

        public static OutputPipe<TInput> GoneWhen<TInput>(
            this IOutputPipe<TInput> pipe, Func<bool> invalidCheck, object error)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status410Gone, error);

        public static OutputPipe<TInput> GoneWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<Task<bool>> invalidCheck,
            Func<TInput, object> errorFactory = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status410Gone, errorFactory);

        public static OutputPipe<TInput> GoneWhen<TInput>(
            this IOutputPipe<TInput> pipe, Func<Task<bool>> invalidCheck, object error)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status410Gone, error);
    }
}
