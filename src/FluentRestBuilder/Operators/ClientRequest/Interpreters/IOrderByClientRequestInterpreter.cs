// <copyright file="IOrderByClientRequestInterpreter.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators.ClientRequest.Interpreters
{
    using System.Collections.Generic;
    using Requests;

    public interface IOrderByClientRequestInterpreter
    {
        IEnumerable<OrderByRequest> ParseRequestQuery(ICollection<string> supportedOrderBys);
    }
}
