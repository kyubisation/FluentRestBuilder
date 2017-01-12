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
        OutputPipe<TInput> Create(
            Func<ClaimsPrincipal, TInput, bool> predicate,
            object error,
            IOutputPipe<TInput> parent);
    }
}