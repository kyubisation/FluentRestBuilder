// ReSharper disable once CheckNamespace
namespace KyubiCode.FluentRest
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using SourcePipes.EntityCollection;

    public static partial class Integration
    {
        public static EntityCollectionSource<TEntity> SelectEntityCollection<TEntity>(
            this ControllerBase controller)
            where TEntity : class =>
            controller.HttpContext.RequestServices
                .GetService<IEntityCollectionSourceFactory<TEntity>>()
                .Resolve();
    }
}
