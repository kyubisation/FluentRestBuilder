// <copyright file="NoContentResultFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Results.NoContent
{
    public class NoContentResultFactory<TInput> : INoContentResultFactory<TInput>
        where TInput : class
    {
        public ResultBase<TInput> Create(IOutputPipe<TInput> parent) =>
            new NoContentResult<TInput>(parent);
    }
}