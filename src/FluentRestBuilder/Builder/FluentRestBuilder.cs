// <copyright file="FluentRestBuilder.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Builder
{
    using Microsoft.Extensions.DependencyInjection;

    public class FluentRestBuilder : IFluentRestBuilder
    {
        public FluentRestBuilder(IServiceCollection services)
        {
            this.Services = services;
            new FluentRestBuilderCore(this.Services)
                .RegisterStorage()
                .RegisterSource()
                .RegisterLazySource()
                .RegisterActionPipe()
                .RegisterClaimValidationPipe()
                .RegisterEntityValidationPipe()
                .RegisterFilterByClientRequestPipe()
                .RegisterFirstOrDefaultPipe()
                .RegisterMappingPipe()
                .RegisterOrderByClientRequestPipe()
                .RegisterPaginationByClientRequestPipe()
                .RegisterQueryablePipe()
                .RegisterSearchByClientRequestPipe()
                .RegisterSingleOrDefaultPipe()
                .RegisterValidationPipe()
                .RegisterAcceptedResult()
                .RegisterCreatedEntityResult()
                .RegisterNoContentResult()
                .RegisterOptionsResultPipe();
        }

        public IServiceCollection Services { get; }
    }
}