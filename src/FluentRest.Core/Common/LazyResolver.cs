namespace KyubiCode.FluentRest.Common
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    public class LazyResolver<TService> : Lazy<TService>
        where TService : class
    {
        public LazyResolver(IServiceProvider provider)
            : base(provider.GetService<TService>)
        {
        }
    }
}
