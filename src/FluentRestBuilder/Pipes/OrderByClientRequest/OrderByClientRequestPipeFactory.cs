namespace FluentRestBuilder.Pipes.OrderByClientRequest
{
    using System.Collections.Generic;
    using System.Linq;

    public class OrderByClientRequestPipeFactory<TInput> : IOrderByClientRequestPipeFactory<TInput>
    {
        private readonly IOrderByClientRequestInterpreter interpreter;

        public OrderByClientRequestPipeFactory(IOrderByClientRequestInterpreter interpreter)
        {
            this.interpreter = interpreter;
        }

        public OutputPipe<IQueryable<TInput>> Resolve(
            IOrderByExpression<TInput> defaultOrderBy,
            IDictionary<string, IOrderByExpressionBuilder<TInput>> orderByDictionary,
            IOutputPipe<IQueryable<TInput>> parent) =>
            new OrderByClientRequestPipe<TInput>(
                defaultOrderBy, orderByDictionary, this.interpreter, parent);
    }
}