// <copyright file="PaginationByClientRequestPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.PaginationByClientRequest
{
    using System.Linq;
    using Microsoft.Extensions.Logging;
    using Storage;

    public class PaginationByClientRequestPipeFactory<TInput> : IPaginationByClientRequestPipeFactory<TInput>
    {
        private readonly IPaginationByClientRequestInterpreter interpreter;
        private readonly IScopedStorage<PaginationMetaInfo> paginationMetaInfoStorage;
        private readonly IQueryableTransformer<TInput> queryableTransformer;
        private readonly ILogger<PaginationByClientRequestPipe<TInput>> logger;

        public PaginationByClientRequestPipeFactory(
            IPaginationByClientRequestInterpreter interpreter,
            IScopedStorage<PaginationMetaInfo> paginationMetaInfoStorage,
            IQueryableTransformer<TInput> queryableTransformer,
            ILogger<PaginationByClientRequestPipe<TInput>> logger = null)
        {
            this.interpreter = interpreter;
            this.paginationMetaInfoStorage = paginationMetaInfoStorage;
            this.queryableTransformer = queryableTransformer;
            this.logger = logger;
        }

        public OutputPipe<IQueryable<TInput>> Create(
            PaginationOptions options,
            IOutputPipe<IQueryable<TInput>> parent) =>
            new PaginationByClientRequestPipe<TInput>(
                options,
                this.interpreter,
                this.paginationMetaInfoStorage,
                this.queryableTransformer,
                this.logger,
                parent);
    }
}