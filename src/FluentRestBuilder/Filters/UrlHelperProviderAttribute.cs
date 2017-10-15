// <copyright file="UrlHelperProviderAttribute.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Filters
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.DependencyInjection;
    using Storage;

    public class UrlHelperProviderAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!(context.Controller is ControllerBase controller))
            {
                throw new FilterRequiresControllerException();
            }

            var storage = controller.HttpContext.RequestServices
                .GetRequiredService<IScopedStorage<IUrlHelper>>();
            storage.Value = controller.Url;
        }
    }
}
