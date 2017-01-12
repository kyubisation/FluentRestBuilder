// <copyright file="PaginationByClientRequestPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.PaginationByClientRequest
{
    using System.Linq;
    using Storage;

    public class PaginationByClientRequestPipeFactory<TInput> : IPaginationByClientRequestPipeFactory<TInput>
    {
        private readonly IPaginationByClientRequestInterpreter interpreter;
        private readonly IScopedStorage<PaginationMetaInfo> paginationMetaInfoStorage;
        private readonly IQueryableTransformer<TInput> queryableTransformer;

        public PaginationByClientRequestPipeFactory(
            IPaginationByClientRequestInterpreter interpreter,
            IScopedStorage<PaginationMetaInfo> paginationMetaInfoStorage,
            IQueryableTransformer<TInput> queryableTransformer)
        {
            this.interpreter = interpreter;
            this.paginationMetaInfoStorage = paginationMetaInfoStorage;
            this.queryableTransformer = queryableTransformer;
        }

        public OutputPipe<IQueryable<TInput>> Create(
            PaginationOptions options,
            IOutputPipe<IQueryable<TInput>> parent) =>
            new PaginationByClientRequestPipe<TInput>(
                options,
                this.interpreter,
                this.paginationMetaInfoStorage,
                this.queryableTransformer,
                parent);
    }
}