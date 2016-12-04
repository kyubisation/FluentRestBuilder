// <copyright file="IEntityUpdatePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.Pipes.Update
{
    using EntityFrameworkCore.Pipes.Update;

    public interface IEntityUpdatePipeFactory<TInput>
        where TInput : class
    {
        EntityUpdatePipe<TInput> Resolve(IOutputPipe<TInput> parent);
    }
}
