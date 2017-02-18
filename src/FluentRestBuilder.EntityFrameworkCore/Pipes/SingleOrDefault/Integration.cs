// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System.Linq;
    using EntityFrameworkCore.MetaModel;
    using Microsoft.Extensions.DependencyInjection;

    public static partial class Integration
    {
        /// <summary>
        /// Retrieves a single value from the provided <see cref="IQueryable{TInput}"/> or
        /// null, if empty.
        /// Throws an exception, if the result of the primary key selection contains more
        /// than one element.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="primaryKey">The primary key value or values.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> SingleOrDefault<TInput>(
            this IOutputPipe<IQueryable<TInput>> pipe, params object[] primaryKey)
            where TInput : class
        {
            var factory = pipe.GetService<IPrimaryKeyExpressionFactory<TInput>>();
            return pipe.SingleOrDefault(factory.CreatePrimaryKeyFilterExpression(primaryKey));
        }
    }
}
