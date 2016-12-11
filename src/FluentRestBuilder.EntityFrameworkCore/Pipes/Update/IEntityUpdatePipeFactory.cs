// <copyright file="IEntityUpdatePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.EntityFrameworkCore.Pipes.Update
{
    using Core;

    public interface IEntityUpdatePipeFactory<TInput>
        where TInput : class
    {
        EntityUpdatePipe<TInput> Resolve(IOutputPipe<TInput> parent);
    }
}
