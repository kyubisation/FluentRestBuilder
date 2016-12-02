namespace KyubiCode.FluentRest.ChainPipes.Deletion
{
    using Microsoft.EntityFrameworkCore;

    public class EntityDeletionPipeFactory<TInput> : IEntityDeletionPipeFactory<TInput>
        where TInput : class
    {
        private readonly DbContext context;

        public EntityDeletionPipeFactory(DbContext context)
        {
            this.context = context;
        }

        public EntityDeletionPipe<TInput> Resolve(IOutputPipe<TInput> parent) =>
            new EntityDeletionPipe<TInput>(this.context, parent);
    }
}