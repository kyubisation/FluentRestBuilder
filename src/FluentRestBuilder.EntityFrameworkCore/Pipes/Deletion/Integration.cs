// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using EntityFrameworkCore.Pipes.Deletion;
    using Microsoft.Extensions.DependencyInjection;

    public static partial class Integration
    {
        public static OutputPipe<TInput> DeleteEntity<TInput>(this IOutputPipe<TInput> pipe)
            where TInput : class =>
            pipe.GetRequiredService<IEntityDeletionPipeFactory<TInput>>()
                .Resolve(pipe);
    }
}
