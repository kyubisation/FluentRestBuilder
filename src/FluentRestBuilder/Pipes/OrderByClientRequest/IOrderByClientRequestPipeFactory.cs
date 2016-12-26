namespace FluentRestBuilder.Pipes.OrderByClientRequest
{
    using System.Collections.Generic;
    using System.Linq;
    using Expressions;

    public interface IOrderByClientRequestPipeFactory<TInput>
    {
        OutputPipe<IQueryable<TInput>> Resolve(
            IOrderByExpression<TInput> defaultOrderBy,
            IDictionary<string, IOrderByExpressionFactory<TInput>> orderByDictionary,
            IOutputPipe<IQueryable<TInput>> parent);
    }
}
