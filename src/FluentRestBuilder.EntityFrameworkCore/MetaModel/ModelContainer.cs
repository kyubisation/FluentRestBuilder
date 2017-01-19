// <copyright file="ModelContainer.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.MetaModel
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.Extensions.DependencyInjection;
    using Storage;

    public class ModelContainer : IModelContainer
    {
        public ModelContainer(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider
                    .GetRequiredService<IScopedStorage<DbContext>>();
                this.Model = context.Value.Model;
            }
        }

        public IModel Model { get; }
    }
}