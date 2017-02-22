// <copyright file="IToListPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.ToList
{
    using System.Collections.Generic;
    using System.Linq;

    public interface IToListPipeFactory<TInput>
    {
        OutputPipe<List<TInput>> Create(IOutputPipe<IQueryable<TInput>> parent);
    }
}
