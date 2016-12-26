namespace FluentRestBuilder.Pipes.OrderByClientRequest
{
    using System;
    using System.Collections.Generic;
    using RestCollectionMutators.OrderBy;

    public interface IOrderByClientRequestInterpreter
    {
        IEnumerable<Tuple<string, OrderByDirection>> ParseRequestQuery();
    }
}
