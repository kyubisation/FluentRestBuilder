// <copyright file="IPaginationByClientRequestInterpreter.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators.ClientRequest.Interpreters
{
    using Requests;

    public interface IPaginationByClientRequestInterpreter
    {
        PaginationRequest ParseRequestQuery();
    }
}
