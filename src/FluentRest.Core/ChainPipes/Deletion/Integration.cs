// ReSharper disable once CheckNamespace
namespace KyubiCode.FluentRest
{
    using ChainPipes.Deletion;

    public static partial class Integration
    {
        public static EntityDeletionPipe<TInput> DeleteEntity<TInput>(this IOutputPipe<TInput> pipe)
            where TInput : class =>
            pipe.GetRequiredItem<IEntityDeletionPipeFactory<TInput>>()
                .Resolve(pipe);
    }
}
