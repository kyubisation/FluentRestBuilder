// ReSharper disable once CheckNamespace
namespace KyubiCode.FluentRest
{
    using ChainPipes.Insertion;
    using Microsoft.Extensions.DependencyInjection;

    public static partial class Integration
    {
        public static EntityInsertionPipe<TInput> InsertEntity<TInput>(
            this IOutputPipe<TInput> pipe)
            where TInput : class =>
            pipe.GetRequiredService<IEntityInsertionPipeFactory<TInput>>()
                .Resolve(pipe);
    }
}
