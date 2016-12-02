namespace KyubiCode.FluentRest.ChainPipes.CollectionTransformation
{
    using System.Collections.Generic;
    using SourcePipes.EntityCollection;
    using Transformers.Hal;

    public interface IRestCollectionLinkGenerator
    {
        IEnumerable<NamedLink> GenerateLinks(PaginationMetaInfo paginationMetaInfo);
    }
}
