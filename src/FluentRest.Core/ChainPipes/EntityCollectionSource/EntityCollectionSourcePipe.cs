namespace KyubiCode.FluentRest.ChainPipes.EntityCollectionSource
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using SourcePipes.EntityCollection;

    public class EntityCollectionSourcePipe<TInput, TOutput> :
        EntityCollectionSource<TOutput>,
        IInputPipe<TInput>
        where TOutput : class
    {
        private readonly Func<IQueryable<TOutput>, TInput, IQueryable<TOutput>> queryablePipe;
        private readonly IOutputPipe<TInput> parent;

        public EntityCollectionSourcePipe(
            Func<IQueryable<TOutput>, TInput, IQueryable<TOutput>> queryablePipe,
            IQueryable<TOutput> queryable,
            IOutputPipe<TInput> parent)
            : base(queryable, parent)
        {
            this.queryablePipe = queryablePipe;
            this.parent = parent;
            this.parent.Attach(this);
        }

        Task<IActionResult> IPipe.Execute() => this.parent.Execute();

        Task<IActionResult> IInputPipe<TInput>.Execute(TInput input)
        {
            if (this.queryablePipe != null)
            {
                this.Queryable = this.queryablePipe(this.Queryable, input);
            }

            return this.ExecuteAsync();
        }
    }
}
