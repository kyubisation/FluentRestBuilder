namespace KyubiCode.FluentRest.ChainPipes.EntityCollectionSource
{
    using System;
    using System.Linq;
    using FluentRest.Common;

    public interface IEntityCollectionSourcePipeFactory<TInput, TOutput>
        where TOutput : class
    {
        EntityCollectionSourcePipe<TInput, TOutput> Resolve(
            Func<IQueryableFactory, IQueryable<TOutput>> selection,
            Func<IQueryable<TOutput>, TInput, IQueryable<TOutput>> queryablePipe,
            IOutputPipe<TInput> pipe);
    }
}
