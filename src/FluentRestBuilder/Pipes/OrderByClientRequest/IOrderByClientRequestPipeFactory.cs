namespace FluentRestBuilder.Pipes.OrderByClientRequest
{
    using System.Collections.Generic;
    using System.Linq;

    public interface IOrderByClientRequestPipeFactory<TInput>
    {
        OutputPipe<IQueryable<TInput>> Resolve(
            IOrderByExpression<TInput> defaultOrderBy,
            IDictionary<string, IOrderByExpressionBuilder<TInput>> orderByDictionary,
            IOutputPipe<IQueryable<TInput>> parent);
    }
}
