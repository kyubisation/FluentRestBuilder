// <copyright file="IEntitySourceFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Sources.EntitySource
{
    using System;
    using System.Linq.Expressions;
    using Microsoft.AspNetCore.Mvc;

    public interface IEntitySourceFactory<TOutput>
    {
        OutputPipe<TOutput> Create(
            ControllerBase controller, Expression<Func<TOutput, bool>> predicate);
    }
}
