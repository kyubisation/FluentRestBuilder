// <copyright file="IValidationPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.Validation
{
    using System;
    using System.Threading.Tasks;

    public interface IValidationPipeFactory<TInput>
        where TInput : class
    {
        OutputPipe<TInput> Create(
            Func<Task<bool>> invalidCheck,
            int statusCode,
            object error,
            IOutputPipe<TInput> parent);
    }
}
