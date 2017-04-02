// <copyright file="OptionsResultFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Results.Options
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.Logging;

    public class OptionsResultFactory<TInput> : IOptionsResultFactory<TInput>
        where TInput : class
    {
        private readonly ILogger<OptionsResult<TInput>> logger;
        private readonly IHttpVerbDictionary httpVerbDictionary;

        public OptionsResultFactory(
            IHttpVerbDictionary httpVerbDictionary,
            ILogger<OptionsResult<TInput>> logger = null)
        {
            this.logger = logger;
            this.httpVerbDictionary = httpVerbDictionary;
        }

        public ResultBase<TInput> Create(
            Func<TInput, IEnumerable<HttpVerb>> verbGeneration,
            IOutputPipe<TInput> parent) =>
            new OptionsResult<TInput>(
                verbGeneration, this.httpVerbDictionary, this.logger, parent);
    }
}