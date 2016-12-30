// <copyright file="PaginationByClientRequestPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.PaginationByClientRequest
{
    using System.Linq;
    using System.Threading.Tasks;
    using Common;
    using Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Storage;

    public class PaginationByClientRequestPipe<TInput> : BaseMappingPipe<IQueryable<TInput>, IQueryable<TInput>>
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
            IOutputPipe<IQueryable<TInput>> parent)
            : base(parent)
        {
            this.options = options ?? new PaginationOptions();
            this.interpreter = interpreter;
            this.paginationMetaInfoStorage = paginationMetaInfoStorage;
            this.queryableTransformer = queryableTransformer;
        }

        protected override async Task<IActionResult> Execute(IQueryable<TInput> input)
        {
            try
            {
                return await base.Execute(input);
            }
            catch (PaginationException exception)
            {
                return new BadRequestObjectResult(new { error = exception.Message });
            }
        }

        protected override async Task<IQueryable<TInput>> MapAsync(IQueryable<TInput> input)
        {
            var paginationRequest = this.interpreter.ParseRequestQuery();
            var paginationValues = new PaginationValues
            {
                Page = paginationRequest.Page ?? 1,
                EntriesPerPage = this.ResolveEntriesPerPage(paginationRequest)
            };
            await this.CalculateMetaInfo(input, paginationValues);

            return input
                .Skip(paginationValues.PageOffset)
                .Take(paginationValues.EntriesPerPage);
        }

        private int ResolveEntriesPerPage(PaginationRequest request)
        {
            var entriesPerPage = request.EntriesPerPage
                ?? this.options.DefaultEntriesPerPage;
            if (entriesPerPage > this.options.MaxEntriesPerPage)
            {
                throw new MaxEntriesPerPageExceededException(
                    entriesPerPage, this.options.MaxEntriesPerPage);
            }

            return entriesPerPage;
        }

        private async Task CalculateMetaInfo(
            IQueryable<TInput> queryable, PaginationValues paginationValues)
        {
            var count = await this.queryableTransformer.Count(queryable);
            this.paginationMetaInfoStorage.Value = new PaginationMetaInfo(
                count, paginationValues.Page, paginationValues.EntriesPerPage);
        }

        private class PaginationValues
        {
            public int Page { get; set; }

            public int EntriesPerPage { get; set; }

            public int PageOffset => this.EntriesPerPage * (this.Page - 1);
        }
    }
}
