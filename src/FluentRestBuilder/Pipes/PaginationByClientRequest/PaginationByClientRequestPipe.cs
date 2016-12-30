// <copyright file="PaginationByClientRequestPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.PaginationByClientRequest
{
    using System.Linq;
    using System.Threading.Tasks;
    using Exceptions;
    using Microsoft.AspNetCore.Mvc;

    public class PaginationByClientRequestPipe<TInput> : BaseMappingPipe<IQueryable<TInput>, IQueryable<TInput>>
    {
        private readonly PaginationOptions options;
        private readonly IPaginationByClientRequestInterpreter interpreter;

        public PaginationByClientRequestPipe(
            PaginationOptions options,
            IPaginationByClientRequestInterpreter interpreter,
            IOutputPipe<IQueryable<TInput>> parent)
            : base(parent)
        {
            this.options = options ?? new PaginationOptions();
            this.interpreter = interpreter;
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

        protected override IQueryable<TInput> Map(IQueryable<TInput> input)
        {
            var paginationRequest = this.interpreter.ParseRequestQuery();
            var page = paginationRequest.Page ?? 1;
            var entriesPerPage = this.ResolveEntriesPerPage(paginationRequest);
            var pageOffset = entriesPerPage * (page - 1);
            return input.Skip(pageOffset).Take(entriesPerPage);
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
    }
}
