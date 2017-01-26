// <copyright file="IInputEntryAccessPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Pipes.InputEntryAccess
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    public interface IInputEntryAccessPipeFactory<TInput>
        where TInput : class
    {
        OutputPipe<TInput> Create(
            Func<EntityEntry<TInput>, Task> entryAction, IOutputPipe<TInput> parent);
    }
}
