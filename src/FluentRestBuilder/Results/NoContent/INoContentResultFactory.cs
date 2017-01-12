// <copyright file="INoContentResultFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Results.NoContent
{
    public interface INoContentResultFactory<TInput>
        where TInput : class
    {
        ResultBase<TInput> Create(IOutputPipe<TInput> parent);
    }
}
