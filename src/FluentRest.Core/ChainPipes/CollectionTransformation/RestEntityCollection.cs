namespace KyubiCode.FluentRest.ChainPipes.CollectionTransformation
{
    using System.Collections.Generic;
    using Transformers.Hal;

    public class RestEntityCollection : RestEntity
    {
        public RestEntityCollection()
        {
            this.Links = new Dictionary<string, object>();
            this.Embedded = new Dictionary<string, object>();
        }
    }
}
