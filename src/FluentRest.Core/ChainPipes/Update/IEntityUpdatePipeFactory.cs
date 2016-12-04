// <copyright file="IEntityUpdatePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.ChainPipes.Update
{
    public interface IEntityUpdatePipeFactory<TInput>
        where TInput : class
    {
        EntityUpdatePipe<TInput> Resolve(IOutputPipe<TInput> parent);
    }
}
