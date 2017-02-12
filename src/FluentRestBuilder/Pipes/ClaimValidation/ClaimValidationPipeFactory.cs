// <copyright file="ClaimValidationPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.ClaimValidation
{
    using System;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Http;
    using Storage;

    public class ClaimValidationPipeFactory<TInput> : IClaimValidationPipeFactory<TInput>
        where TInput : class
    {
        private readonly ClaimsPrincipal user;

        public ClaimValidationPipeFactory(IScopedStorage<HttpContext> httpContextStorage)
        {
            this.user = httpContextStorage.Value.User;
        }

        public OutputPipe<TInput> Create(
                Func<ClaimsPrincipal, TInput, bool> predicate,
                Func<TInput, object> errorFactory,
                IOutputPipe<TInput> parent) =>
            new ClaimValidationPipe<TInput>(predicate, this.user, errorFactory, parent);
    }
}
