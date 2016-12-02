namespace KyubiCode.FluentRest.ChainPipes.Deletion
{
    public interface IEntityDeletionPipeFactory<TInput>
        where TInput : class
    {
        EntityDeletionPipe<TInput> Resolve(IOutputPipe<TInput> parent);
    }
}
