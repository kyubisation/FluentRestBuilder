// <copyright file="OptionsResultFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Results.Options
{
    using System;
    using System.Collections.Generic;

    public class OptionsResultFactory<TInput> : IOptionsResultFactory<TInput>
        where TInput : class
    {
        private readonly IHttpVerbMap httpVerbMap;

        public OptionsResultFactory(
            IHttpVerbMap httpVerbMap)
        {
            this.httpVerbMap = httpVerbMap;
        }

        public ResultBase<TInput> Create(
            Func<TInput, IEnumerable<HttpVerb>> verbGeneration,
            IOutputPipe<TInput> parent) =>
            new OptionsResult<TInput>(
                verbGeneration,
                this.httpVerbMap,
                parent);
    }
}