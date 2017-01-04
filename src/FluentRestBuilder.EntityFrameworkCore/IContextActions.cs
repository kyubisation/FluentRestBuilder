// <copyright file="IContextActions.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore
{
    using System.Threading.Tasks;

    public interface IContextActions
    {
        Task<int> RemoveAndSave<TEntity>(TEntity entity)
            where TEntity : class;

        Task<int> AddAndSave<TEntity>(TEntity entity)
            where TEntity : class;

        Task<int> UpdateAndSave<TEntity>(TEntity entity)
            where TEntity : class;
    }
}
