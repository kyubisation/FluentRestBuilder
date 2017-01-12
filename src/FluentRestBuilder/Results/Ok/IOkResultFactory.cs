// <copyright file="IOkResultFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Results.Ok
{
    public interface IOkResultFactory<TInput>
        where TInput : class
    {
        ResultBase<TInput> Resolve(IOutputPipe<TInput> parent);
    }
}
