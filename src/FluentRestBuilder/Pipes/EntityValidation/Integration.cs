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

        public static OutputPipe<TEntity> InvalidWhen<TEntity>(
            this IOutputPipe<TEntity> pipe,
            Func<TEntity, Task<bool>> invalidCheck,
            int statusCode,
            object error = null)
            where TEntity : class =>
            pipe.GetService<IEntityValidationPipeFactory<TEntity>>()
                .Create(invalidCheck, statusCode, error, pipe);

        public static OutputPipe<TEntity> InvalidWhen<TEntity>(
            this IOutputPipe<TEntity> pipe,
            Func<TEntity, bool> invalidCheck,
            int statusCode,
            object error = null)
            where TEntity : class =>
            pipe.InvalidWhen(e => Task.FromResult(invalidCheck(e)), statusCode, error);

        public static OutputPipe<TEntity> ForbiddenWhen<TEntity>(
            this IOutputPipe<TEntity> pipe,
            Func<TEntity, bool> invalidCheck,
            object error = null)
            where TEntity : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status403Forbidden, error);

        public static OutputPipe<TEntity> ForbiddenWhen<TEntity>(
            this IOutputPipe<TEntity> pipe,
            Func<TEntity, Task<bool>> invalidCheck,
            object error = null)
            where TEntity : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status403Forbidden, error);

        public static OutputPipe<TEntity> BadRequestWhen<TEntity>(
            this IOutputPipe<TEntity> pipe,
            Func<TEntity, bool> invalidCheck,
            object error = null)
            where TEntity : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status400BadRequest, error);

        public static OutputPipe<TEntity> BadRequestWhen<TEntity>(
            this IOutputPipe<TEntity> pipe,
            Func<TEntity, Task<bool>> invalidCheck,
            object error = null)
            where TEntity : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status400BadRequest, error);

        public static OutputPipe<TEntity> NotFoundWhenEmpty<TEntity>(
            this IOutputPipe<TEntity> pipe, object error = null)
            where TEntity : class =>
            InvalidWhen(pipe, e => e == null, StatusCodes.Status404NotFound, error);

        public static OutputPipe<TEntity> NotFoundWhen<TEntity>(
            this IOutputPipe<TEntity> pipe,
            Func<TEntity, bool> invalidCheck,
            object error = null)
            where TEntity : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status404NotFound, error);

        public static OutputPipe<TEntity> NotFoundWhen<TEntity>(
            this IOutputPipe<TEntity> pipe,
            Func<TEntity, Task<bool>> invalidCheck,
            object error = null)
            where TEntity : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status404NotFound, error);

        public static OutputPipe<TEntity> GoneWhen<TEntity>(
            this IOutputPipe<TEntity> pipe,
            Func<TEntity, bool> invalidCheck,
            object error = null)
            where TEntity : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status410Gone, error);

        public static OutputPipe<TEntity> GoneWhen<TEntity>(
            this IOutputPipe<TEntity> pipe,
            Func<TEntity, Task<bool>> invalidCheck,
            object error = null)
            where TEntity : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status410Gone, error);
    }
}
