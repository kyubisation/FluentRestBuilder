// ReSharper disable once CheckNamespace
namespace KyubiCode.FluentRest
{
    using System;
    using System.Linq;
    using ChainPipes.EntityCollectionSource;
    using Common;
    using Microsoft.Extensions.DependencyInjection;

    public static partial class Integration
    {
        public static EntityCollectionSourcePipe<TInput, TOutput> CreateEntityCollectionSource<TInput, TOutput>(
            this IOutputPipe<TInput> pipe,
            Func<IQueryableFactory, IQueryable<TOutput>> selection,
            Func<IQueryable<TOutput>, TInput, IQueryable<TOutput>> queryablePipe = null)
            where TOutput : class =>
            pipe.GetRequiredService<IEntityCollectionSourcePipeFactory<TInput, TOutput>>()
                .Resolve(selection, queryablePipe, pipe);
    }
}
