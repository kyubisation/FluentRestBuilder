// <copyright file="IPaginationByClientRequestPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.PaginationByClientRequest
{
    using System.Linq;

    public interface IPaginationByClientRequestPipeFactory<TInput>
    {
        OutputPipe<IQueryable<TInput>> Create(
            PaginationOptions options,
            IOutputPipe<IQueryable<TInput>> parent);
    }
}
