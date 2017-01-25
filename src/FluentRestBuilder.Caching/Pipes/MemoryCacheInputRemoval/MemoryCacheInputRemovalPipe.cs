// <copyright file="MemoryCacheInputRemovalPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Pipes.MemoryCacheInputRemoval
{
    using System;
    using System.Threading.Tasks;
    using FluentRestBuilder.Pipes;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;

    public class MemoryCacheInputRemovalPipe<TInput> : ChainPipe<TInput>
    {
        private readonly Func<TInput, object> keyFactory;
        private readonly IMemoryCache memoryCache;

        public MemoryCacheInputRemovalPipe(
            Func<TInput, object> keyFactory,
            IMemoryCache memoryCache,
            IOutputPipe<TInput> parent)
            : base(parent)
        {
            this.keyFactory = keyFactory;
            this.memoryCache = memoryCache;
        }

        protected override Task<IActionResult> Execute(TInput input)
        {
            this.RemoveFromCache(input);
            return base.Execute(input);
        }

        private void RemoveFromCache(TInput input)
        {
            var key = this.keyFactory(input);
            this.memoryCache.Remove(key);
        }
    }
}
