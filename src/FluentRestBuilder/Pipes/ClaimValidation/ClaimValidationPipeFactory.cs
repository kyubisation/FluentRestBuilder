// <copyright file="ClaimValidationPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.ClaimValidation
{
    using System;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Http;

    public class ClaimValidationPipeFactory<TInput> : IClaimValidationPipeFactory<TInput>
        where TInput : class
    {
        private readonly ClaimsPrincipal user;

        public ClaimValidationPipeFactory(IHttpContextAccessor httpContextAccessor)
        {
            this.user = httpContextAccessor.HttpContext.User;
        }

        public OutputPipe<TInput> Resolve(
                Func<ClaimsPrincipal, TInput, bool> predicate,
                object error,
                IOutputPipe<TInput> parent) =>
            new ClaimValidationPipe<TInput>(predicate, this.user, error, parent);
    }
}
