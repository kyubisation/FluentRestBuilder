// ReSharper disable once CheckNamespace
namespace KyubiCode.FluentRest
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    public static partial class Integration
    {
        public static ChainPipes.Validation.ValidationPipe<TInput> InvalidWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<bool> invalidCheck,
            int statusCode,
            object error = null)
            where TInput : class =>
            new ChainPipes.Validation.ValidationPipe<TInput>(invalidCheck, statusCode, error, pipe);

        public static ChainPipes.Validation.ValidationPipe<TInput> InvalidWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<Task<bool>> invalidCheck,
            int statusCode,
            object error = null)
            where TInput : class =>
            new ChainPipes.Validation.ValidationPipe<TInput>(invalidCheck, statusCode, error, pipe);

        public static ChainPipes.Validation.ValidationPipe<TInput> ForbiddenWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<bool> invalidCheck,
            object error = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status403Forbidden, error);

        public static ChainPipes.Validation.ValidationPipe<TInput> ForbiddenWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<Task<bool>> invalidCheck,
            object error = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status403Forbidden, error);

        public static ChainPipes.Validation.ValidationPipe<TInput> BadRequestWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<bool> invalidCheck,
            object error = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status400BadRequest, error);

        public static ChainPipes.Validation.ValidationPipe<TInput> BadRequestWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<Task<bool>> invalidCheck,
            object error = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status400BadRequest, error);

        public static ChainPipes.Validation.ValidationPipe<TInput> NotFoundWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<bool> invalidCheck,
            object error = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status404NotFound, error);

        public static ChainPipes.Validation.ValidationPipe<TInput> NotFoundWhen<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<Task<bool>> invalidCheck,
            object error = null)
            where TInput : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status404NotFound, error);
    }
}
