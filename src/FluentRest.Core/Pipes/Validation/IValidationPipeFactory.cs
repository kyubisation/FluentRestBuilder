// <copyright file="IValidationPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.Pipes.Validation
{
    using System;
    using System.Threading.Tasks;

    public interface IValidationPipeFactory<TInput>
        where TInput : class
    {
        ValidationPipe<TInput> Resolve(
            Func<Task<bool>> invalidCheck,
            int statusCode,
            object error,
            IOutputPipe<TInput> parent);

        ValidationPipe<TInput> Resolve(
            Func<bool> invalidCheck,
            int statusCode,
            object error,
            IOutputPipe<TInput> parent);
    }
}
