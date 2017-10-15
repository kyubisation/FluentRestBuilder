// <copyright file="IOrderByExpressionDictionary.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators.ClientRequest.OrderByExpressions
{
    using System.Collections.Generic;

    public interface IOrderByExpressionDictionary<TSource>
        : IDictionary<string, IOrderByExpressionFactory<TSource>>
    {
    }
}
