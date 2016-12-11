// <copyright file="IClaimValidationPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.ClaimValidation
{
    using System;
    using System.Security.Claims;

    public interface IClaimValidationPipeFactory<TInput>
        where TInput : class
    {
        ClaimValidationPipe<TInput> Resolve(
            Func<ClaimsPrincipal, TInput, bool> predicate,
            object error,
            IOutputPipe<TInput> parent);

        ClaimValidationPipe<TInput> Resolve(
            Func<ClaimsPrincipal, bool> predicate,
            object error,
            IOutputPipe<TInput> parent);

        ClaimValidationPipe<TInput> Resolve(
            string claimType,
            Func<TInput, string> claim,
            object error,
            IOutputPipe<TInput> parent);

        ClaimValidationPipe<TInput> Resolve(
            string claimType,
            string claim,
            object error,
            IOutputPipe<TInput> parent);
    }
}