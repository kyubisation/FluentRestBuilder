// ReSharper disable once CheckNamespace
namespace KyubiCode.FluentRest
{
    using System;
    using System.Threading.Tasks;
    using ChainPipes.Actions;

    public static partial class Integration
    {
        public static ActionPipe<TInput> Do<TInput>(
            this IOutputPipe<TInput> pipe, Action<TInput> action)
            where TInput : class =>
            new ActionPipe<TInput>(action, pipe);

        public static ActionPipe<TInput> Do<TInput>(
            this IOutputPipe<TInput> pipe, Func<TInput, Task> action)
            where TInput : class =>
            new ActionPipe<TInput>(action, pipe);
    }
}
