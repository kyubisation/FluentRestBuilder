// <copyright file="ICreatedEntityResultFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Results.CreatedEntity
{
    using System;

    public interface ICreatedEntityResultFactory<TInput>
        where TInput : class
    {
        ResultBase<TInput> Resolve(
            Func<TInput, object> routeValuesGenerator,
            string routeName,
            IOutputPipe<TInput> parent);
    }
}
