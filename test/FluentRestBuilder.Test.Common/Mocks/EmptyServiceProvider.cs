namespace FluentRestBuilder.Test.Common.Mocks
{
    using System;

    public class EmptyServiceProvider : IServiceProvider
    {
        public object GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }
    }
}
