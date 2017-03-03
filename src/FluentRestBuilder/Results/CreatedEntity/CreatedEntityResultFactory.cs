// <copyright file="CreatedEntityResultFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Results.CreatedEntity
{
    using System;
    using Microsoft.Extensions.Logging;

    public class CreatedEntityResultFactory<TInput> : ICreatedEntityResultFactory<TInput>
        where TInput : class
    {
        private readonly ILogger<CreatedEntityResult<TInput>> logger;

        public CreatedEntityResultFactory(ILogger<CreatedEntityResult<TInput>> logger = null)
        {
            this.logger = logger;
        }

        public ResultBase<TInput> Create(
            Func<TInput, object> routeValuesFactory,
            string routeName,
            IOutputPipe<TInput> parent) =>
            new CreatedEntityResult<TInput>(routeValuesFactory, routeName, this.logger, parent);
    }
}