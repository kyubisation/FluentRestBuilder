// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRest
{
    using Core;
    using Core.ChainPipes.Update;
    using Microsoft.Extensions.DependencyInjection;

    public static partial class Integration
    {
        public static EntityUpdatePipe<TInput> UpdateEntity<TInput>(
            this IOutputPipe<TInput> pipe)
            where TInput : class =>
            pipe.GetRequiredService<IEntityUpdatePipeFactory<TInput>>()
                .Resolve(pipe);
    }
}
