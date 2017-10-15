// <copyright file="IFilterByClientRequestInterpreter.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators.ClientRequest.Interpreters
{
    using System.Collections.Generic;
    using Requests;

    public interface IFilterByClientRequestInterpreter
    {
        IEnumerable<FilterRequest> ParseRequestQuery(ICollection<string> supportedFilters);
    }
}
