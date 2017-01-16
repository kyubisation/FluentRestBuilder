// <copyright file="IDbContextContainer.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore
{
    using Microsoft.EntityFrameworkCore;

    public interface IDbContextContainer
    {
        DbContext Context { get; }
    }
}
