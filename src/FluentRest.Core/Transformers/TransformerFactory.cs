namespace KyubiCode.FluentRest.Transformers
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    public class TransformerFactory : ITransformerFactory
    {
        private readonly IServiceProvider serviceProvider;

        public TransformerFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public ITransformerFactory<TInput> Resolve<TInput>() =>
            this.serviceProvider.GetRequiredService<ITransformerFactory<TInput>>();
    }

    public class TransformerFactory<TInput> : ITransformerFactory<TInput>
    {
        private readonly IServiceProvider serviceProvider;

        public TransformerFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public ITransformer<TInput, TOutput> Resolve<TOutput>() =>
            this.serviceProvider.GetService<ITransformer<TInput, TOutput>>();
    }
}