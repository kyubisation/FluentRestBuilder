// <copyright file="UrlHelperStorage.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage.Storage
{
    using FluentRestBuilder.Storage;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.Routing;

    public class UrlHelperStorage : ScopedStorage<IUrlHelper>
    {
        public UrlHelperStorage(
            IActionContextAccessor actionContextAccessor,
            IUrlHelperFactory urlHelperFactory)
        {
            this.Value = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);
        }
    }
}
