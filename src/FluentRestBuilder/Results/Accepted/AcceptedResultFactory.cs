// <copyright file="AcceptedResultFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Results.Accepted
{
    public class AcceptedResultFactory<TInput> : IAcceptedResultFactory<TInput>
        where TInput : class
    {
        public ResultBase<TInput> Create(IOutputPipe<TInput> parent) =>
            new AcceptedResult<TInput>(parent);
    }
}