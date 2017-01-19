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
        public static OutputPipe<TInput> SingleOrDefault<TInput>(
            this IOutputPipe<IQueryable<TInput>> pipe, params object[] primaryKey)
            where TInput : class
        {
            var factory = pipe.GetService<IPrimaryKeyExpressionFactory<TInput>>();
            return pipe.SingleOrDefault(factory.CreatePrimaryKeyFilterExpression(primaryKey));
        }
    }
}
