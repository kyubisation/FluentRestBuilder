// <copyright file="EntityValidationPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.EntityValidation
{
    using System;
    using System.Threading.Tasks;

    public class EntityValidationPipe<TInput> : ValidationPipeBase<TInput>
        where TInput : class
    {
        private readonly Func<TInput, Task<bool>> invalidCheck;

        public EntityValidationPipe(
            Func<TInput, Task<bool>> invalidCheck,
            int statusCode,
            object error,
            IOutputPipe<TInput> parent)
            : base(statusCode, error, parent)
        {
            this.invalidCheck = invalidCheck;
        }

        protected override Task<bool> IsInvalid(TInput entity) => this.invalidCheck(entity);
    }
}
