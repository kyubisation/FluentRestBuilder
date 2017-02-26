// <copyright file="MappingPipeBase.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public abstract class MappingPipeBase<TInput, TOutput> : OutputPipe<TOutput>, IInputPipe<TInput>
    {
        private readonly IOutputPipe<TInput> parent;

        protected MappingPipeBase(ILogger logger, IOutputPipe<TInput> parent)
            : base(logger, parent)
        {
            this.parent = parent;
            this.parent.Attach(this);
        }

        Task<IActionResult> IInputPipe<TInput>.Execute(TInput input)
        {
            this.Logger.Debug?.Log(
                "Entering method {0} of pipe {1} with input",
                nameof(IInputPipe<TInput>.Execute),
                this.GetType());
            this.Logger.Trace?.Log("Received input in pipe {0} is {1}", this.GetType(), input);
            var result = this.Execute(input);
            this.Logger.Debug?.Log(
                "Exiting method {0} of pipe {1} with input",
                nameof(IInputPipe<TInput>.Execute),
                this.GetType());
            return result;
        }

        protected virtual async Task<IActionResult> Execute(TInput input)
        {
            Check.IsPipeAttached(this.Child);
            TOutput result;
            try
            {
                result = await this.MapAsync(input);
                this.Logger.Debug?.Log("Mapped {0} to {1}", typeof(TInput), typeof(TOutput));
                this.Logger.Trace?.Log("Mapped output is {0}", result);
            }
            catch (Exception exception)
            {
                this.Logger.Error?.Log(
                    0, exception, "Mapping {0} to {1} failed", typeof(TInput), typeof(TOutput));
                throw;
            }

            return await this.Child.Execute(result);
        }

        protected override Task<IActionResult> Execute() => this.parent.Execute();

        protected virtual Task<TOutput> MapAsync(TInput input) =>
            Task.FromResult(this.Map(input));

        protected virtual TOutput Map(TInput input)
        {
            throw new NotImplementedException();
        }
    }
}
