// <copyright file="ClaimValidationPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.ClaimValidation
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    public class ClaimValidationPipe<TInput> : ValidationPipeBase<TInput>
        where TInput : class
    {
        private readonly ClaimsPrincipal principal;
        private readonly Func<ClaimsPrincipal, TInput, bool> validCheck;

        public ClaimValidationPipe(
            Func<ClaimsPrincipal, TInput, bool> validCheck,
            ClaimsPrincipal principal,
            object error,
            IOutputPipe<TInput> parent)
            : base(StatusCodes.Status403Forbidden, error, parent)
        {
            this.principal = principal;
            this.validCheck = validCheck;
        }

        protected override Task<bool> IsInvalid(TInput entity) =>
            Task.FromResult(!this.validCheck(this.principal, entity));
    }
}
