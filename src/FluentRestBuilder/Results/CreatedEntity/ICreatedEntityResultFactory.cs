// <copyright file="ICreatedEntityResultFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Results.CreatedEntity
{
    using System;

    public interface ICreatedEntityResultFactory<TInput>
        where TInput : class
    {
        ResultBase<TInput> Create(
            Func<TInput, object> routeValuesFactory,
            string routeName,
            IOutputPipe<TInput> parent);
    }
}
