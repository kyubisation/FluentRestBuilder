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
        public static IFluentRestBuilderCoreConfiguration RegisterEntityValidationPipe(
            this IFluentRestBuilderCoreConfiguration builder)
        {
            builder.Services.TryAddSingleton(
                typeof(IEntityValidationPipeFactory<>),
                typeof(EntityValidationPipeFactory<>));
            return builder;
        }

        /// <summary>
        /// Invalidate the input on the given condition.
        /// Results in a action result with the given status code on failure.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="invalidCheck">The invalidation check.</param>
        /// <param name="statusCode">The HTTP status code to be used on invalidation.</param>
        /// <param name="errorFactory">A factory function to create the error object.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> InvalidWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<TInput, Task<bool>> invalidCheck,
            int statusCode,
            Func<TInput, object> errorFactory = null)
            where TInput : class
        {
            var factory = pipe.GetService<IEntityValidationPipeFactory<TInput>>();
            Check.IsPipeRegistered(factory, typeof(EntityValidationPipe<>));
            return factory.Create(invalidCheck, statusCode, errorFactory, pipe);
        }

        /// <summary>
        /// Invalidate the input on the given condition.
        /// Results in a action result with the given status code on failure.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="invalidCheck">The invalidation check.</param>
        /// <param name="statusCode">The HTTP status code to be used on invalidation.</param>
        /// <param name="error">The error object.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> InvalidWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<TInput, Task<bool>> invalidCheck,
            int statusCode,
            object error)
            where TInput : class =>
            pipe.InvalidWhen(invalidCheck, statusCode, i => error);

        /// <summary>
        /// Invalidate the input on the given condition.
        /// Results in a action result with the given status code on failure.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="invalidCheck">The invalidation check.</param>
        /// <param name="statusCode">The HTTP status code to be used on invalidation.</param>
        /// <param name="errorFactory">A factory function to create the error object.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> InvalidWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<TInput, bool> invalidCheck,
            int statusCode,
            Func<TInput, object> errorFactory = null)
            where TInput : class =>
            pipe.InvalidWhen(e => Task.FromResult(invalidCheck(e)), statusCode, errorFactory);

        /// <summary>
        /// Invalidate the input on the given condition.
        /// Results in a action result with the given status code on failure.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="invalidCheck">The invalidation check.</param>
        /// <param name="statusCode">The HTTP status code to be used on invalidation.</param>
        /// <param name="error">The error object.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> InvalidWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<TInput, bool> invalidCheck,
            int statusCode,
            object error)
            where TInput : class =>
            pipe.InvalidWhen(e => Task.FromResult(invalidCheck(e)), statusCode, error);

        /// <summary>
        /// Invalidate the input on the given condition.
        /// Results in a action result with the given status code on failure.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="invalidCheck">The invalidation check.</param>
        /// <param name="statusCode">The HTTP status code to be used on invalidation.</param>
        /// <param name="errorFactory">A factory function to create the error object.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> InvalidWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<Task<bool>> invalidCheck,
            int statusCode,
            Func<TInput, object> errorFactory = null)
            where TInput : class =>
            pipe.InvalidWhen(e => invalidCheck(), statusCode, errorFactory);

        /// <summary>
        /// Invalidate the input on the given condition.
        /// Results in a action result with the given status code on failure.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="invalidCheck">The invalidation check.</param>
        /// <param name="statusCode">The HTTP status code to be used on invalidation.</param>
        /// <param name="error">The error object.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> InvalidWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<Task<bool>> invalidCheck,
            int statusCode,
            object error)
            where TInput : class =>
            pipe.InvalidWhen(e => invalidCheck(), statusCode, error);

        /// <summary>
        /// Invalidate the input on the given condition.
        /// Results in a action result with the given status code on failure.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="invalidCheck">The invalidation check.</param>
        /// <param name="statusCode">The HTTP status code to be used on invalidation.</param>
        /// <param name="errorFactory">A factory function to create the error object.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> InvalidWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<bool> invalidCheck,
            int statusCode,
            Func<TInput, object> errorFactory = null)
            where TInput : class =>
            pipe.InvalidWhen(() => Task.FromResult(invalidCheck()), statusCode, errorFactory);

        /// <summary>
        /// Invalidate the input on the given condition.
        /// Results in a action result with the given status code on failure.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="invalidCheck">The invalidation check.</param>
        /// <param name="statusCode">The HTTP status code to be used on invalidation.</param>
        /// <param name="error">The error object.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> InvalidWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<bool> invalidCheck,
            int statusCode,
            object error)
            where TInput : class =>
            pipe.InvalidWhen(() => Task.FromResult(invalidCheck()), statusCode, error);

        /// <summary>
        /// Invalidate the input on the given condition.
        /// Results in a forbidden (403) action result on failure.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="invalidCheck">The invalidation check.</param>
        /// <param name="errorFactory">A factory function to create the error object.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> ForbiddenWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<TInput, bool> invalidCheck,
            Func<TInput, object> errorFactory = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status403Forbidden, errorFactory);

        /// <summary>
        /// Invalidate the input on the given condition.
        /// Results in a forbidden (403) action result on failure.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="invalidCheck">The invalidation check.</param>
        /// <param name="error">The error object.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> ForbiddenWhen<TInput>(
            this IOutputPipe<TInput> pipe, Func<TInput, bool> invalidCheck, object error)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status403Forbidden, error);

        /// <summary>
        /// Invalidate the input on the given condition.
        /// Results in a forbidden (403) action result on failure.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="invalidCheck">The invalidation check.</param>
        /// <param name="errorFactory">A factory function to create the error object.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> ForbiddenWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<TInput, Task<bool>> invalidCheck,
            Func<TInput, object> errorFactory = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status403Forbidden, errorFactory);

        /// <summary>
        /// Invalidate the input on the given condition.
        /// Results in a forbidden (403) action result on failure.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="invalidCheck">The invalidation check.</param>
        /// <param name="error">The error object.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> ForbiddenWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<TInput, Task<bool>> invalidCheck,
            object error)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status403Forbidden, error);

        /// <summary>
        /// Invalidate the input on the given condition.
        /// Results in a forbidden (403) action result on failure.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="invalidCheck">The invalidation check.</param>
        /// <param name="errorFactory">A factory function to create the error object.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> ForbiddenWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<bool> invalidCheck,
            Func<TInput, object> errorFactory = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status403Forbidden, errorFactory);

        /// <summary>
        /// Invalidate the input on the given condition.
        /// Results in a forbidden (403) action result on failure.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="invalidCheck">The invalidation check.</param>
        /// <param name="error">The error object.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> ForbiddenWhen<TInput>(
            this IOutputPipe<TInput> pipe, Func<bool> invalidCheck, object error)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status403Forbidden, error);

        /// <summary>
        /// Invalidate the input on the given condition.
        /// Results in a forbidden (403) action result on failure.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="invalidCheck">The invalidation check.</param>
        /// <param name="errorFactory">A factory function to create the error object.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> ForbiddenWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<Task<bool>> invalidCheck,
            Func<TInput, object> errorFactory = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status403Forbidden, errorFactory);

        /// <summary>
        /// Invalidate the input on the given condition.
        /// Results in a forbidden (403) action result on failure.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="invalidCheck">The invalidation check.</param>
        /// <param name="error">The error object.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> ForbiddenWhen<TInput>(
            this IOutputPipe<TInput> pipe, Func<Task<bool>> invalidCheck, object error)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status403Forbidden, error);

        /// <summary>
        /// Invalidate the input on the given condition.
        /// Results in a bad request (400) action result on failure.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="invalidCheck">The invalidation check.</param>
        /// <param name="errorFactory">A factory function to create the error object.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> BadRequestWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<TInput, bool> invalidCheck,
            Func<TInput, object> errorFactory = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status400BadRequest, errorFactory);

        /// <summary>
        /// Invalidate the input on the given condition.
        /// Results in a bad request (400) action result on failure.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="invalidCheck">The invalidation check.</param>
        /// <param name="error">The error object.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> BadRequestWhen<TInput>(
            this IOutputPipe<TInput> pipe, Func<TInput, bool> invalidCheck, object error)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status400BadRequest, error);

        /// <summary>
        /// Invalidate the input on the given condition.
        /// Results in a bad request (400) action result on failure.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="invalidCheck">The invalidation check.</param>
        /// <param name="errorFactory">A factory function to create the error object.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> BadRequestWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<TInput, Task<bool>> invalidCheck,
            Func<TInput, object> errorFactory = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status400BadRequest, errorFactory);

        /// <summary>
        /// Invalidate the input on the given condition.
        /// Results in a bad request (400) action result on failure.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="invalidCheck">The invalidation check.</param>
        /// <param name="error">The error object.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> BadRequestWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<TInput, Task<bool>> invalidCheck,
            object error)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status400BadRequest, error);

        /// <summary>
        /// Invalidate the input on the given condition.
        /// Results in a bad request (400) action result on failure.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="invalidCheck">The invalidation check.</param>
        /// <param name="errorFactory">A factory function to create the error object.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> BadRequestWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<bool> invalidCheck,
            Func<TInput, object> errorFactory = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status400BadRequest, errorFactory);

        /// <summary>
        /// Invalidate the input on the given condition.
        /// Results in a bad request (400) action result on failure.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="invalidCheck">The invalidation check.</param>
        /// <param name="error">The error object.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> BadRequestWhen<TInput>(
            this IOutputPipe<TInput> pipe, Func<bool> invalidCheck, object error)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status400BadRequest, error);

        /// <summary>
        /// Invalidate the input on the given condition.
        /// Results in a bad request (400) action result on failure.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="invalidCheck">The invalidation check.</param>
        /// <param name="errorFactory">A factory function to create the error object.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> BadRequestWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<Task<bool>> invalidCheck,
            Func<TInput, object> errorFactory = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status400BadRequest, errorFactory);

        /// <summary>
        /// Invalidate the input on the given condition.
        /// Results in a bad request (400) action result on failure.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="invalidCheck">The invalidation check.</param>
        /// <param name="error">The error object.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> BadRequestWhen<TInput>(
            this IOutputPipe<TInput> pipe, Func<Task<bool>> invalidCheck, object error)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status400BadRequest, error);

        /// <summary>
        /// Invalidate the input if it is null.
        /// Results in a not found (404) action result on failure.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="error">The error object.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> NotFoundWhenEmpty<TInput>(
            this IOutputPipe<TInput> pipe, object error = null)
            where TInput : class =>
            InvalidWhen(pipe, e => e == null, StatusCodes.Status404NotFound, error);

        /// <summary>
        /// Invalidate the input on the given condition.
        /// Results in a not found (404) action result on failure.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="invalidCheck">The invalidation check.</param>
        /// <param name="errorFactory">A factory function to create the error object.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> NotFoundWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<TInput, bool> invalidCheck,
            Func<TInput, object> errorFactory = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status404NotFound, errorFactory);

        /// <summary>
        /// Invalidate the input on the given condition.
        /// Results in a not found (404) action result on failure.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="invalidCheck">The invalidation check.</param>
        /// <param name="error">The error object.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> NotFoundWhen<TInput>(
            this IOutputPipe<TInput> pipe, Func<TInput, bool> invalidCheck, object error)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status404NotFound, error);

        /// <summary>
        /// Invalidate the input on the given condition.
        /// Results in a not found (404) action result on failure.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="invalidCheck">The invalidation check.</param>
        /// <param name="errorFactory">A factory function to create the error object.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> NotFoundWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<TInput, Task<bool>> invalidCheck,
            Func<TInput, object> errorFactory = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status404NotFound, errorFactory);

        /// <summary>
        /// Invalidate the input on the given condition.
        /// Results in a not found (404) action result on failure.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="invalidCheck">The invalidation check.</param>
        /// <param name="error">The error object.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> NotFoundWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<TInput, Task<bool>> invalidCheck,
            object error)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status404NotFound, error);

        /// <summary>
        /// Invalidate the input on the given condition.
        /// Results in a not found (404) action result on failure.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="invalidCheck">The invalidation check.</param>
        /// <param name="errorFactory">A factory function to create the error object.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> NotFoundWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<bool> invalidCheck,
            Func<TInput, object> errorFactory = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status404NotFound, errorFactory);

        /// <summary>
        /// Invalidate the input on the given condition.
        /// Results in a not found (404) action result on failure.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="invalidCheck">The invalidation check.</param>
        /// <param name="error">The error object.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> NotFoundWhen<TInput>(
            this IOutputPipe<TInput> pipe, Func<bool> invalidCheck, object error)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status404NotFound, error);

        /// <summary>
        /// Invalidate the input on the given condition.
        /// Results in a not found (404) action result on failure.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="invalidCheck">The invalidation check.</param>
        /// <param name="errorFactory">A factory function to create the error object.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> NotFoundWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<Task<bool>> invalidCheck,
            Func<TInput, object> errorFactory = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status404NotFound, errorFactory);

        /// <summary>
        /// Invalidate the input on the given condition.
        /// Results in a not found (404) action result on failure.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="invalidCheck">The invalidation check.</param>
        /// <param name="error">The error object.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> NotFoundWhen<TInput>(
            this IOutputPipe<TInput> pipe, Func<Task<bool>> invalidCheck, object error)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status404NotFound, error);

        /// <summary>
        /// Invalidate the input on the given condition.
        /// Results in a gone (410) action result on failure.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="invalidCheck">The invalidation check.</param>
        /// <param name="errorFactory">A factory function to create the error object.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> GoneWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<TInput, bool> invalidCheck,
            Func<TInput, object> errorFactory = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status410Gone, errorFactory);

        /// <summary>
        /// Invalidate the input on the given condition.
        /// Results in a gone (410) action result on failure.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="invalidCheck">The invalidation check.</param>
        /// <param name="error">The error object.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> GoneWhen<TInput>(
            this IOutputPipe<TInput> pipe, Func<TInput, bool> invalidCheck, object error)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status410Gone, error);

        /// <summary>
        /// Invalidate the input on the given condition.
        /// Results in a gone (410) action result on failure.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="invalidCheck">The invalidation check.</param>
        /// <param name="errorFactory">A factory function to create the error object.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> GoneWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<TInput, Task<bool>> invalidCheck,
            Func<TInput, object> errorFactory = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status410Gone, errorFactory);

        /// <summary>
        /// Invalidate the input on the given condition.
        /// Results in a gone (410) action result on failure.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="invalidCheck">The invalidation check.</param>
        /// <param name="error">The error object.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> GoneWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<TInput, Task<bool>> invalidCheck,
            object error)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status410Gone, error);

        /// <summary>
        /// Invalidate the input on the given condition.
        /// Results in a gone (410) action result on failure.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="invalidCheck">The invalidation check.</param>
        /// <param name="errorFactory">A factory function to create the error object.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> GoneWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<bool> invalidCheck,
            Func<TInput, object> errorFactory = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status410Gone, errorFactory);

        /// <summary>
        /// Invalidate the input on the given condition.
        /// Results in a gone (410) action result on failure.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="invalidCheck">The invalidation check.</param>
        /// <param name="error">The error object.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> GoneWhen<TInput>(
            this IOutputPipe<TInput> pipe, Func<bool> invalidCheck, object error)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status410Gone, error);

        /// <summary>
        /// Invalidate the input on the given condition.
        /// Results in a gone (410) action result on failure.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="invalidCheck">The invalidation check.</param>
        /// <param name="errorFactory">A factory function to create the error object.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> GoneWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<Task<bool>> invalidCheck,
            Func<TInput, object> errorFactory = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status410Gone, errorFactory);

        /// <summary>
        /// Invalidate the input on the given condition.
        /// Results in a gone (410) action result on failure.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="invalidCheck">The invalidation check.</param>
        /// <param name="error">The error object.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> GoneWhen<TInput>(
            this IOutputPipe<TInput> pipe, Func<Task<bool>> invalidCheck, object error)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status410Gone, error);
    }
}
