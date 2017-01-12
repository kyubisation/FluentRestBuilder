// <copyright file="IOptionsResultFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Results.Options
{
    using System;
    using System.Collections.Generic;

    public interface IOptionsResultFactory<TInput>
        where TInput : class
    {
        ResultBase<TInput> Create(
            Func<TInput, IEnumerable<HttpVerb>> verbGeneration,
            IOutputPipe<TInput> parent);
    }
}
