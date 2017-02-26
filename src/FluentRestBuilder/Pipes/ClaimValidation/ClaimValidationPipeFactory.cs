// <copyright file="ClaimValidationPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.ClaimValidation
{
    using System;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Storage;

    public class ClaimValidationPipeFactory<TInput> : IClaimValidationPipeFactory<TInput>
        where TInput : class
    {
        private readonly ILogger<ClaimValidationPipe<TInput>> logger;
        private readonly ClaimsPrincipal user;

        public ClaimValidationPipeFactory(
            IScopedStorage<HttpContext> httpContextStorage,
            ILogger<ClaimValidationPipe<TInput>> logger = null)
        {
            this.logger = logger;
            this.user = httpContextStorage.Value.User;
        }

        public OutputPipe<TInput> Create(
                Func<ClaimsPrincipal, TInput, bool> predicate,
                Func<TInput, object> errorFactory,
                IOutputPipe<TInput> parent) =>
            new ClaimValidationPipe<TInput>(
                predicate, this.user, errorFactory, this.logger, parent);
    }
}
