namespace KyubiCode.FluentRest.ChainPipes.Update
{
    public interface IEntityUpdatePipeFactory<TInput>
        where TInput : class
    {
        EntityUpdatePipe<TInput> Resolve(IOutputPipe<TInput> parent);
    }
}
