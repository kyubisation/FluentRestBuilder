// <copyright file="IQueryableSourceFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Sources.QueryableSource
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public interface IQueryableSourceFactory<TOutput>
    {
        OutputPipe<TOutput> Resolve(ControllerBase controller);
    }
}
