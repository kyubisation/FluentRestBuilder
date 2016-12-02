namespace KyubiCode.FluentRest.ChainPipes.Update
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Common;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public class EntityUpdatePipe<TInput> : InputOutputPipe<TInput>, IItemProvider
        where TInput : class
    {
        private readonly DbContext context;
        private readonly IOutputPipe<TInput> parent;
        private TInput storedEntity;

        public EntityUpdatePipe(
            DbContext context,
            IOutputPipe<TInput> parent)
            : base(parent)
        {
            this.context = context;
            this.parent = parent;
        }

        object IItemProvider.GetItem(Type itemType) =>
            itemType == typeof(TInput) ? this.storedEntity : this.parent.GetItem(itemType);

        protected override async Task<IActionResult> ExecuteAsync(TInput entity)
        {
            try
            {
                await this.context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }

            var queryable = this.GetService<IQueryable<TInput>>();
            if (queryable != null)
            {
                this.storedEntity = await queryable.SingleAsync();
            }

            return null;
        }
    }
}
