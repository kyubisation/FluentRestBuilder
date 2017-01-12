// <copyright file="OptionsResultFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Results.Options
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Http;
    using Storage;

    public class OptionsResultFactory<TInput> : IOptionsResultFactory<TInput>
        where TInput : class
    {
        private readonly IHttpVerbMap httpVerbMap;
        private readonly IScopedStorage<HttpContext> httpContextStorage;

        public OptionsResultFactory(
            IHttpVerbMap httpVerbMap,
            IScopedStorage<HttpContext> httpContextStorage)
        {
            this.httpVerbMap = httpVerbMap;
            this.httpContextStorage = httpContextStorage;
        }

        public ResultBase<TInput> Create(
            Func<TInput, IEnumerable<HttpVerb>> verbGeneration,
            IOutputPipe<TInput> parent) =>
            new OptionsResult<TInput>(
                verbGeneration,
                this.httpVerbMap,
                this.httpContextStorage,
                parent);
    }
}