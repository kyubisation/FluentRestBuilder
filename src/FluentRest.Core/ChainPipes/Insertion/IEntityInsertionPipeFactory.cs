namespace KyubiCode.FluentRest.ChainPipes.Insertion
{
    public interface IEntityInsertionPipeFactory<TInput>
        where TInput : class
    {
        EntityInsertionPipe<TInput> Resolve(IOutputPipe<TInput> parent);
    }
}
