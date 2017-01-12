// <copyright file="IAcceptedResultFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Results.Accepted
{
    public interface IAcceptedResultFactory<TInput>
        where TInput : class
    {
        ResultBase<TInput> Create(IOutputPipe<TInput> parent);
    }
}
