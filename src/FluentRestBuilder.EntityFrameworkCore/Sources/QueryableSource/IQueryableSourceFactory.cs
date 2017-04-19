// <copyright file="IQueryableSourceFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Sources.QueryableSource
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;

    public interface IQueryableSourceFactory<TOutput>
    {
        OutputPipe<IQueryable<TOutput>> Create(ControllerBase controller);
    }
}
