// <copyright file="FluentRestBuilderConfiguration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Builder
{
    using Microsoft.Extensions.DependencyInjection;

    public class FluentRestBuilderConfiguration : IFluentRestBuilderConfiguration
    {
        public FluentRestBuilderConfiguration(IServiceCollection services)
        {
            this.Services = services;
            new FluentRestBuilderCoreConfiguration(this.Services)
                .RegisterSource()
                .RegisterLazySource()
                .RegisterActionPipe()
                .RegisterFilterByClientRequestPipe()
                .RegisterFirstOrDefaultPipe()
                .RegisterMappingPipe()
                .RegisterOrderByClientRequestPipe()
                .RegisterPaginationByClientRequestPipe()
                .RegisterQueryablePipe()
                .RegisterSearchByClientRequestPipe()
                .RegisterSingleOrDefaultPipe()
                .RegisterToListPipe()
                .RegisterAcceptedResult()
                .RegisterCreatedEntityResult()
                .RegisterNoContentResult()
                .RegisterOkResult()
                .RegisterOptionsResultPipe();
        }

        public IServiceCollection Services { get; }
    }
}