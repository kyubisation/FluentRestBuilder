// ReSharper disable once CheckNamespace
namespace KyubiCode.FluentRest
{
    using ChainPipes.Update;
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
