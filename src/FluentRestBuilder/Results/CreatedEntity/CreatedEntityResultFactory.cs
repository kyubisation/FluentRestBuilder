// <copyright file="CreatedEntityResultFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Results.CreatedEntity
{
    using System;

    public class CreatedEntityResultFactory<TInput> : ICreatedEntityResultFactory<TInput>
        where TInput : class
    {
        public ResultBase<TInput> Resolve(
            Func<TInput, object> routeValuesGenerator,
            string routeName,
            IOutputPipe<TInput> parent) =>
            new CreatedEntityResult<TInput>(routeValuesGenerator, routeName, parent);
    }
}