namespace KyubiCode.FluentRest.Transformers.Hal
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;

    public interface ILinkGenerator<in TEntity>
    {
        IEnumerable<NamedLink> GenerateLinks(IUrlHelper urlHelper, TEntity entity);
    }
}
