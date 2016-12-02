namespace KyubiCode.FluentRest.ResultPipes.Common
{
    using System;
    using System.Threading.Tasks;
    using FluentRest.Common;
    using Microsoft.AspNetCore.Mvc;

    public abstract class ResultPipe<TInput> : InputPipe<TInput>, IInputPipe<TInput>
        where TInput : class
    {
        protected ResultPipe(IOutputPipe<TInput> parent)
            : base(parent)
        {
        }

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
