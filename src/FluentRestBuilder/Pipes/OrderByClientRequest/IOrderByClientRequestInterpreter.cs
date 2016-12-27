// <copyright file="IOrderByClientRequestInterpreter.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.OrderByClientRequest
{
    using System;
    using System.Collections.Generic;

    public interface IOrderByClientRequestInterpreter
    {
        IEnumerable<Tuple<string, OrderByDirection>> ParseRequestQuery();
    }
}
