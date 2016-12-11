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
        EntityValidationPipe<TInput> Resolve(
            Func<TInput, Task<bool>> invalidCheck,
            int statusCode,
            object error,
            IOutputPipe<TInput> parent);

        EntityValidationPipe<TInput> Resolve(
            Func<TInput, bool> invalidCheck,
            int statusCode,
            object error,
            IOutputPipe<TInput> parent);
    }
}
