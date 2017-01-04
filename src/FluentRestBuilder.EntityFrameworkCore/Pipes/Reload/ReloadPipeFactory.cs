// <copyright file="ReloadPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Pipes.Reload
{
    public class ReloadPipeFactory<TInput> : IReloadPipeFactory<TInput>
        where TInput : class
    {
        private readonly IContextActions contextActions;

        public ReloadPipeFactory(IContextActions contextActions)
        {
            this.contextActions = contextActions;
        }

        public OutputPipe<TInput> Resolve(IOutputPipe<TInput> parent) =>
            new ReloadPipe<TInput>(this.contextActions, parent);
    }
}