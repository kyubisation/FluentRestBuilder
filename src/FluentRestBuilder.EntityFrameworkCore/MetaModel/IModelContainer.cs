// <copyright file="IModelContainer.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.EntityFrameworkCore.MetaModel
{
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore.Metadata;

    // ReSharper disable once UnusedTypeParameter
    public interface IModelContainer<TEntity>
    {
        IEntityType EntityType { get; }

        IKey PrimaryKey { get; }

        IReadOnlyCollection<IProperty> Properties { get; }

        IReadOnlyCollection<INavigation> Navigations { get; }
    }
}
