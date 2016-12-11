// <copyright file="MockResultPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Test.Mocks
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public class MockResultPipe<TInput> : IInputPipe<TInput>
        where TInput : class
    {
        private readonly IOutputPipe<TInput> parent;

        public MockResultPipe(IOutputPipe<TInput> parent)
        {
            this.parent = parent;
            this.parent.Attach(this);
        }

        public TInput Input { get; private set; }

        public object GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }

        public object GetItem(Type itemType)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Execute(TInput input)
        {
            this.Input = input;
            return Task.FromResult(new MockActionResult() as IActionResult);
        }

        public Task<IActionResult> Execute() => this.parent.Execute();
    }
}
