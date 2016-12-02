namespace KyubiCode.FluentRest.ChainPipes.Update
{
    using Microsoft.EntityFrameworkCore;

    public class EntityUpdatePipeFactory<TInput> : IEntityUpdatePipeFactory<TInput>
        where TInput : class
    {
        private readonly DbContext context;

        public EntityUpdatePipeFactory(DbContext context)
        {
            this.context = context;
        }

        public EntityUpdatePipe<TInput> Resolve(IOutputPipe<TInput> parent) =>
            new EntityUpdatePipe<TInput>(this.context, parent);
    }
}