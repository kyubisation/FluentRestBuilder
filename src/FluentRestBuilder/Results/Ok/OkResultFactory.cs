// <copyright file="OkResultFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Results.Ok
{
    public class OkResultFactory<TInput> : IOkResultFactory<TInput>
        where TInput : class
    {
        public ResultBase<TInput> Create(IOutputPipe<TInput> parent) =>
            new OkResult<TInput>(parent);
    }
}