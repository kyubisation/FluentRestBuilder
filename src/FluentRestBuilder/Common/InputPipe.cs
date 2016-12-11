// <copyright file="InputPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Common
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public abstract class InputPipe<TInput> : IInputPipe<TInput>
    {
        private readonly IOutputPipe<TInput> parent;

        protected InputPipe(IOutputPipe<TInput> parent)
        {
            this.parent = parent;
            this.parent.Attach(this);
        }

        object IServiceProvider.GetService(Type serviceType) =>
            this.parent.GetService(serviceType);

        Task<IActionResult> IInputPipe<TInput>.Execute(TInput input)
        {
            throw new NotImplementedException();
        }

        Task<IActionResult> IPipe.Execute() => this.parent.Execute();
    }
}
