// <copyright file="FluentRestBuilder.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core
{
    using System.Linq;
    using Common;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Pipes.Actions;
    using Pipes.ClaimValidation;
    using Pipes.EntityValidation;
    using Pipes.Transformation;
    using Pipes.Validation;
    using Results.Options;
    using Sources.LazySource;
    using Sources.Source;
    using Storage;
    using Transformers;

    public class FluentRestBuilder : IFluentRestBuilder
    {
        public FluentRestBuilder(IServiceCollection services)
        {
            this.Services = services;
            RegisterPipeFactories(this.Services);
            RegisterTransformations(this.Services);
            RegisterUtilities(this.Services);
        }

        public IServiceCollection Services { get; }

        private static void RegisterPipeFactories(IServiceCollection collection)
        {
            collection.TryAddScoped(
                typeof(ISourcePipeFactory<>), typeof(SourcePipeFactory<>));
            collection.TryAddScoped(
                typeof(ILazySourcePipeFactory<>), typeof(LazySourcePipeFactory<>));
            collection.TryAddScoped(
                typeof(IActionPipeFactory<>), typeof(ActionPipeFactory<>));
            collection.TryAddScoped(
                typeof(IClaimValidationPipeFactory<>), typeof(ClaimValidationPipeFactory<>));
            collection.TryAddScoped(
                typeof(IEntityValidationPipeFactory<>), typeof(EntityValidationPipeFactory<>));
            collection.TryAddScoped(
                typeof(ITransformationPipeFactory<,>), typeof(TransformationPipeFactory<,>));
            collection.TryAddScoped(
                typeof(IValidationPipeFactory<>), typeof(ValidationPipeFactory<>));
        }

        private static void RegisterTransformations(IServiceCollection collection)
        {
            collection.TryAddScoped<ITransformerFactory, TransformerFactory>();
            collection.TryAddScoped(typeof(ITransformerFactory<>), typeof(TransformerFactory<>));
            collection.TryAddTransient(typeof(ITransformationBuilder<>), typeof(TransformationBuilder<>));
        }

        private static void RegisterUtilities(IServiceCollection collection)
        {
            collection.TryAddTransient(typeof(LazyResolver<>));
            collection.TryAddScoped(typeof(IAllowedOptionsBuilder<>), typeof(AllowedOptionsBuilder<>));
            collection.TryAddScoped(typeof(IScopedStorage<>), typeof(ScopedStorage<>));
            collection.TryAddScoped(
                s => s.GetRequiredService<IHttpContextAccessor>().HttpContext.Request.Query);

            collection.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            if (collection.All(d => d.ServiceType != typeof(IUrlHelper)))
            {
                collection.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
                collection.TryAddScoped(serviceProvider =>
                {
                    var actionContextAccessor = serviceProvider.GetService<IActionContextAccessor>();
                    var urlHelperFactory = serviceProvider.GetService<IUrlHelperFactory>();
                    return urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);
                });
            }
        }
    }
}
