// <copyright file="HttpContextProviderAttribute.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Filters
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.DependencyInjection;
    using Storage;

    public class HttpContextProviderAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.Controller as ControllerBase;
            if (controller == null)
            {
                throw new FilterRequiresControllerException();
            }

            var storage = controller.HttpContext.RequestServices
                .GetRequiredService<IScopedStorage<HttpContext>>();
            storage.Value = controller.HttpContext;
        }
    }
}
