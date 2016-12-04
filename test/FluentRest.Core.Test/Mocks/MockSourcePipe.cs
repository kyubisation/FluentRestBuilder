// <copyright file="MockSourcePipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.Test.Mocks
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public class MockSourcePipe<TEntity> : IOutputPipe<TEntity>
        where TEntity : class
    {
        private readonly IServiceProvider serviceProvider;
        private readonly TEntity source;
        private IInputPipe<TEntity> child;

        public MockSourcePipe(
            TEntity source,
            IServiceProvider serviceProvider)
        {
            this.source = source;
            this.serviceProvider = serviceProvider;
        }

        public static MockResultPipe<TTarget> CreateCompleteChain<TTarget>(
            TEntity entity,
            IServiceProvider serviceProvider,
            Func<IOutputPipe<TEntity>, IOutputPipe<TTarget>> pipeCreator)
            where TTarget : class
        {
            var source = new MockSourcePipe<TEntity>(entity, serviceProvider);
            var pipe = pipeCreator(source);
            return new MockResultPipe<TTarget>(pipe);
        }

        public object GetService(Type serviceType) =>
            this.serviceProvider.GetService(serviceType);

        public object GetItem(Type itemType) => null;

        public TPipe Attach<TPipe>(TPipe pipe)
            where TPipe : IInputPipe<TEntity>
        {
            this.child = pipe;
            return pipe;
        }

        public Task<IActionResult> Execute()
        {
            return this.child.Execute(this.source);
        }
    }
}
