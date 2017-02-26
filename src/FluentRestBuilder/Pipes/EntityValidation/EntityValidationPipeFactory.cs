// <copyright file="EntityValidationPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.EntityValidation
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    public class EntityValidationPipeFactory<TInput> : IEntityValidationPipeFactory<TInput>
        where TInput : class
    {
        private readonly ILogger<EntityValidationPipe<TInput>> logger;

        public EntityValidationPipeFactory(ILogger<EntityValidationPipe<TInput>> logger = null)
        {
            this.logger = logger;
        }

        public OutputPipe<TInput> Create(
                Func<TInput, Task<bool>> invalidCheck,
                int statusCode,
                Func<TInput, object> errorFactory,
                IOutputPipe<TInput> parent) =>
            new EntityValidationPipe<TInput>(
                invalidCheck, statusCode, errorFactory, this.logger, parent);
    }
}