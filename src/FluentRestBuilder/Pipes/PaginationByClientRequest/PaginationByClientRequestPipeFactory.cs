// <copyright file="PaginationByClientRequestPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.PaginationByClientRequest
{
    using System.Linq;

    public class PaginationByClientRequestPipeFactory<TInput> : IPaginationByClientRequestPipeFactory<TInput>
    {
        private readonly IPaginationByClientRequestInterpreter interpreter;

        public PaginationByClientRequestPipeFactory(
            IPaginationByClientRequestInterpreter interpreter)
        {
            this.interpreter = interpreter;
        }

        public OutputPipe<IQueryable<TInput>> Resolve(
            PaginationOptions options,
            IOutputPipe<IQueryable<TInput>> parent) =>
            new PaginationByClientRequestPipe<TInput>(options, this.interpreter, parent);
    }
}