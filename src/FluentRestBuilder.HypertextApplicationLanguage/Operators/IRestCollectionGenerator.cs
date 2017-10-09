// <copyright file="IRestCollectionGenerator.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage.Operators
{
    using System;
    using System.Collections.Generic;
    using FluentRestBuilder.Operators.ClientRequest;

    public interface IRestCollectionGenerator<TEntity, in TResponse>
    {
        /// <summary>
        /// Create a REST collection instance with the received entities
        /// and the optional pagination meta infos.
        /// </summary>
        /// <param name="entities">An <see cref="IEnumerable{T}"/> of entities.</param>
        /// <param name="mapping">The mapping function.</param>
        /// <param name="paginationInfo">Pagination infos or null.</param>
        /// <returns>A REST collection instance.</returns>
        IRestEntity CreateCollection(
            IEnumerable<TEntity> entities,
            Func<TEntity, TResponse> mapping,
            PaginationInfo paginationInfo = null);
    }
}
