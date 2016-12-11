// <copyright file="ModelContainer.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.MetaModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.Extensions.DependencyInjection;

    public class ModelContainer<TEntity> : IModelContainer<TEntity>
    {
        public ModelContainer(DbContextOptions contextOptions, IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = (DbContext)scope.ServiceProvider.GetService(contextOptions.ContextType);
                this.EntityType = context.Model.FindEntityType(typeof(TEntity));
                this.PrimaryKey = this.EntityType.FindPrimaryKey();
                this.Properties = this.EntityType.GetProperties().ToImmutableList();
                this.Navigations = this.EntityType.GetNavigations().ToImmutableList();
            }
        }

        public IEntityType EntityType { get; }

        public IKey PrimaryKey { get; }

        public IReadOnlyCollection<IProperty> Properties { get; }

        public IReadOnlyCollection<INavigation> Navigations { get; }
    }
}