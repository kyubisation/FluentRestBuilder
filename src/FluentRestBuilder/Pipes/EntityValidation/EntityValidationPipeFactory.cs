// <copyright file="EntityValidationPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.EntityValidation
{
    using System;
    using System.Threading.Tasks;

    public class EntityValidationPipeFactory<TInput> : IEntityValidationPipeFactory<TInput>
        where TInput : class
    {
        public OutputPipe<TInput> Create(
            Func<TInput, Task<bool>> invalidCheck,
            int statusCode,
            object error,
            IOutputPipe<TInput> parent) =>
            new EntityValidationPipe<TInput>(invalidCheck, statusCode, error, parent);
    }
}