// <copyright file="ResultBase.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Results
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public abstract class ResultBase<TInput> : IInputPipe<TInput>
        where TInput : class
    {
        private readonly IOutputPipe<TInput> parent;

        protected ResultBase(IOutputPipe<TInput> parent)
        {
            this.parent = parent;
            this.parent.Attach(this);
        }

        object IServiceProvider.GetService(Type serviceType) =>
            this.parent.GetService(serviceType);

        Task<IActionResult> IPipe.Execute() => this.parent.Execute();

        Task<IActionResult> IInputPipe<TInput>.Execute(TInput input) =>
            this.CreateResultAsync(input);

        protected virtual Task<IActionResult> CreateResultAsync(TInput source) =>
            Task.FromResult(this.CreateResult(source));

        protected virtual IActionResult CreateResult(TInput source)
        {
            throw new NotImplementedException();
        }
    }
}
