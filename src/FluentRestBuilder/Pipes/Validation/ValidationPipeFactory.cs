// <copyright file="ValidationPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.Validation
{
    using System;
    using System.Threading.Tasks;

    public class ValidationPipeFactory<TInput> : IValidationPipeFactory<TInput>
        where TInput : class
    {
        public OutputPipe<TInput> Resolve(
            Func<Task<bool>> invalidCheck,
            int statusCode,
            object error,
            IOutputPipe<TInput> parent) =>
            new ValidationPipe<TInput>(invalidCheck, statusCode, error, parent);

        public OutputPipe<TInput> Resolve(
            Func<bool> invalidCheck,
            int statusCode,
            object error,
            IOutputPipe<TInput> parent) =>
            new ValidationPipe<TInput>(invalidCheck, statusCode, error, parent);
    }
}