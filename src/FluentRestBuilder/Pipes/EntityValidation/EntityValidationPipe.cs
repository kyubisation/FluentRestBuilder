// <copyright file="EntityValidationPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.EntityValidation
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    public class EntityValidationPipe<TInput> : ValidationPipeBase<TInput>
        where TInput : class
    {
        private readonly Func<TInput, Task<bool>> invalidCheck;

        public EntityValidationPipe(
            Func<TInput, Task<bool>> invalidCheck,
            int statusCode,
            Func<TInput, object> errorFactory,
            ILogger<EntityValidationPipe<TInput>> logger,
            IOutputPipe<TInput> parent)
            : base(statusCode, errorFactory, logger, parent)
        {
            this.invalidCheck = invalidCheck;
        }

        protected override Task<bool> IsInvalid(TInput entity) => this.invalidCheck(entity);
    }
}
