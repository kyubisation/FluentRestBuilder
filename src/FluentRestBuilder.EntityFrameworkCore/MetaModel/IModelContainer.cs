// <copyright file="IModelContainer.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.MetaModel
{
    using Microsoft.EntityFrameworkCore.Metadata;

    public interface IModelContainer
    {
        IModel Model { get; }
    }
}
