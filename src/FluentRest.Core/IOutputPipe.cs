namespace KyubiCode.FluentRest
{
    public interface IOutputPipe<out TOutput> : IPipe
    {
        TPipe Attach<TPipe>(TPipe pipe)
            where TPipe : IInputPipe<TOutput>;
    }
}
