// <copyright file="IFilterByClientRequestInterpreter.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest
{
    using System.Collections.Generic;

    public interface IFilterByClientRequestInterpreter
    {
        IEnumerable<FilterRequest> ParseRequestQuery(IEnumerable<string> supportedFilters);
    }
}
