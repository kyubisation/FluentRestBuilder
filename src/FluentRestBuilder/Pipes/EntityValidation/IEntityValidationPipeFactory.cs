// <copyright file="IEntityValidationPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.EntityValidation
{
    using System;
    using System.Threading.Tasks;

    public interface IEntityValidationPipeFactory<TInput>
        where TInput : class
    {
        OutputPipe<TInput> Resolve(
            Func<TInput, Task<bool>> invalidCheck,
            int statusCode,
            object error,
            IOutputPipe<TInput> parent);
    }
}
