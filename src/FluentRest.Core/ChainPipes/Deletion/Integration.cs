// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRest
{
    using Core;
    using Core.ChainPipes.Deletion;

    public static partial class Integration
    {
        public static EntityDeletionPipe<TInput> DeleteEntity<TInput>(this IOutputPipe<TInput> pipe)
            where TInput : class =>
            ItemProviderExtensions.GetRequiredItem<IEntityDeletionPipeFactory<TInput>>((IItemProvider)pipe)
                .Resolve(pipe);
    }
}
