// <copyright file="IPaginationByClientRequestInterpreter.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.PaginationByClientRequest
{
    public interface IPaginationByClientRequestInterpreter
    {
        PaginationRequest ParseRequestQuery();
    }
}
