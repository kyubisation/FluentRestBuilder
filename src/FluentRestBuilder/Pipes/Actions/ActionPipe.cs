// <copyright file="ActionPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.Pipes.Actions
{
    using System;
    using System.Threading.Tasks;
    using Common;
    using Microsoft.AspNetCore.Mvc;

    public class ActionPipe<TInput> : InputOutputPipe<TInput>
        where TInput : class
    {
        private readonly Func<TInput, Task> action;

        public ActionPipe(
            Func<TInput, Task> action,
            IOutputPipe<TInput> parent)
            : base(parent)
        {
            this.action = action;
        }

        public ActionPipe(
            Action<TInput> action,
            IOutputPipe<TInput> parent)
            : this(WrapActionInAsyncCallback(action), parent)
        {
        }

        protected override async Task<IActionResult> ExecuteAsync(TInput entity)
        {
            await this.action(entity);
            return null;
        }

        private static Func<TInput, Task> WrapActionInAsyncCallback(Action<TInput> action)
        {
            return entity =>
            {
                action(entity);
                return Task.CompletedTask;
            };
        }
    }
}
