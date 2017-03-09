// <copyright file="PaginationByClientRequestPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.PaginationByClientRequest
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Storage;

    public class PaginationByClientRequestPipe<TInput> : MappingPipeBase<IQueryable<TInput>, IQueryable<TInput>>
    {
        private readonly PaginationOptions options;
        private readonly IPaginationByClientRequestInterpreter interpreter;
        private readonly IScopedStorage<PaginationMetaInfo> paginationMetaInfoStorage;
        private readonly IQueryableTransformer<TInput> queryableTransformer;

        public PaginationByClientRequestPipe(
            PaginationOptions options,
            IPaginationByClientRequestInterpreter interpreter,
            IScopedStorage<PaginationMetaInfo> paginationMetaInfoStorage,
            IQueryableTransformer<TInput> queryableTransformer,
            ILogger<PaginationByClientRequestPipe<TInput>> logger,
            IOutputPipe<IQueryable<TInput>> parent)
            : base(logger, parent)
        {
            this.options = options ?? new PaginationOptions();
            this.AssertValidOptions();
            this.interpreter = interpreter;
            this.paginationMetaInfoStorage = paginationMetaInfoStorage;
            this.queryableTransformer = queryableTransformer;
        }

        protected override async Task<IQueryable<TInput>> MapAsync(IQueryable<TInput> input)
        {
            var paginationRequest = this.interpreter.ParseRequestQuery();
            this.Logger.Debug?.Log("Pagination request {0}", paginationRequest);
            var paginationValues = new PaginationValues
            {
                Offset = paginationRequest.Offset ?? 0,
                Limit = this.ResolveLimit(paginationRequest)
            };
            await this.CalculateMetaInfo(input, paginationValues);

            return input
                .Skip(paginationValues.Offset)
                .Take(paginationValues.Limit);
        }

        private void AssertValidOptions()
        {
            if (this.options.MaxLimit < this.options.DefaultLimit)
            {
                throw new InvalidOperationException(
                    $"${nameof(PaginationOptions.MaxLimit)} must not be " +
                    $"smaller than ${nameof(PaginationOptions.DefaultLimit)}!");
            }
        }

        private int ResolveLimit(PaginationRequest request)
        {
            var limit = request.Limit ?? this.options.DefaultLimit;
            return limit > this.options.MaxLimit ? this.options.MaxLimit : limit;
        }

        private async Task CalculateMetaInfo(
            IQueryable<TInput> queryable, PaginationValues paginationValues)
        {
            var count = await this.queryableTransformer.Count(queryable);
            this.paginationMetaInfoStorage.Value = new PaginationMetaInfo(
                count, paginationValues.Offset, paginationValues.Limit);
        }

        private struct PaginationValues
        {
            public int Offset;

            public int Limit;
        }
    }
}
