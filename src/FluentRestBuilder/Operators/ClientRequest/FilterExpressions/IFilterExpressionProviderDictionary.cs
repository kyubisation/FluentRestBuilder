// <copyright file="IFilterExpressionProviderDictionary.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators.ClientRequest.FilterExpressions
{
    using System.Collections.Generic;

    public interface IFilterExpressionProviderDictionary<TEntity>
        : IDictionary<string, IFilterExpressionProvider<TEntity>>
    {
    }
}
