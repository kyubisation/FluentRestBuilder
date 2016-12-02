// ReSharper disable once CheckNamespace
namespace KyubiCode.FluentRest
{
    using Microsoft.AspNetCore.Mvc;
    using SourcePipes.RequestEntity;

    public static partial class Integration
    {
        public static RequestEntitySource<TEntity> From<TEntity>(
            this ControllerBase controller, TEntity entity)
            where TEntity : class =>
            new RequestEntitySource<TEntity>(entity, controller.HttpContext.RequestServices);
    }
}
