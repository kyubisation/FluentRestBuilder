// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System.Linq;
    using Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Pipes;
    using Pipes.PaginationByClientRequest;

    public static partial class Integration
    {
        internal static IFluentRestBuilder RegisterPaginationByClientRequestPipe(
            this IFluentRestBuilder builder)
        {
            builder.Services.TryAddScoped(
                typeof(IPaginationByClientRequestPipeFactory<>),
                typeof(PaginationByClientRequestPipeFactory<>));
            builder.Services.TryAddScoped<
                IPaginationByClientRequestInterpreter, PaginationByClientRequestInterpreter>();
            builder.Services.TryAddSingleton<IQueryArgumentKeys, QueryArgumentKeys>();
            builder.Services.TryAddSingleton(
                typeof(IQueryableTransformer<>), typeof(QueryableTransformer<>));
            return builder;
        }

        public static OutputPipe<IQueryable<TInput>> ApplyPaginationByClientRequest<TInput>(
                this IOutputPipe<IQueryable<TInput>> pipe,
                PaginationOptions options = null) =>
            pipe.GetService<IPaginationByClientRequestPipeFactory<TInput>>()
                .Resolve(options, pipe);
    }
}
