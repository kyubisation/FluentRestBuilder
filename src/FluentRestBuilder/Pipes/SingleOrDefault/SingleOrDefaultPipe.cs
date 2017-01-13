// <copyright file="SingleOrDefaultPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.SingleOrDefault
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class SingleOrDefaultPipe<TInput> : MappingPipeBase<IQueryable<TInput>, TInput>
    {
        private readonly Expression<Func<TInput, bool>> predicate;
        private readonly IQueryableTransformer<TInput> queryableTransformer;

        public SingleOrDefaultPipe(
            Expression<Func<TInput, bool>> predicate,
            IQueryableTransformer<TInput> queryableTransformer,
            IOutputPipe<IQueryable<TInput>> parent)
            : base(parent)
        {
            this.predicate = predicate;
            this.queryableTransformer = queryableTransformer;
        }

        protected override async Task<IActionResult> Execute(IQueryable<TInput> input)
        {
            try
            {
                return await base.Execute(input);
            }
            catch (InvalidOperationException)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        protected override Task<TInput> MapAsync(IQueryable<TInput> input)
        {
            var queryable = input.Where(this.predicate);
            return this.queryableTransformer.SingleOrDefault(queryable);
        }
    }
}
