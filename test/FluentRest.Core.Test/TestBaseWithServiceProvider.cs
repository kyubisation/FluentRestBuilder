namespace KyubiCode.FluentRest.Test
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    public abstract class TestBaseWithServiceProvider : IDisposable
    {
        private Lazy<IServiceProvider> serviceProvider;

        protected TestBaseWithServiceProvider()
        {
            this.serviceProvider = new Lazy<IServiceProvider>(() =>
            {
                var services = new ServiceCollection();
                this.Setup(services);
                return services.BuildServiceProvider();
            });
        }

        public IServiceProvider ServiceProvider => this.serviceProvider?.Value;

        public virtual void Dispose()
        {
            var disposable = this.ServiceProvider as IDisposable;
            if (disposable == null)
            {
                return;
            }

            disposable.Dispose();
            this.serviceProvider = null;
        }

        protected virtual void Setup(IServiceCollection services)
        {
        }
    }
}
