// <copyright file="MockResult.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Mocks
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public class MockResult<TInput> : IInputPipe<TInput>
        where TInput : class
    {
        private readonly IOutputPipe<TInput> parent;

        public MockResult(IOutputPipe<TInput> parent)
        {
            this.parent = parent;
            this.parent.Attach(this);
        }

        public object GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Execute(TInput input) =>
            Task.FromResult((IActionResult)new MockActionResult(input));

        public Task<IActionResult> Execute() => this.parent.Execute();

        private class MockActionResult : ObjectResult
        {
            public MockActionResult(object value)
                : base(value)
            {
            }

            public override Task ExecuteResultAsync(ActionContext context)
            {
                throw new NotImplementedException();
            }
        }
    }
}
