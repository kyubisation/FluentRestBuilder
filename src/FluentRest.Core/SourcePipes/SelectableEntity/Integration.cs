// ReSharper disable once CheckNamespace
namespace KyubiCode.FluentRest
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using MetaModel;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using SourcePipes.SelectableEntity;

    public static partial class Integration
    {
        public static SelectableEntitySource<TEntity> SelectEntity<TEntity>(
            this ControllerBase controller, Expression<Func<TEntity, bool>> selectionFilter)
            where TEntity : class =>
            controller.HttpContext.RequestServices
                .GetRequiredService<ISelectableEntitySourceFactory<TEntity>>()
                .Resolve(selectionFilter);

        public static SelectableEntitySource<TEntity> SelectEntityById<TEntity>(
            this ControllerBase controller, object identifier)
            where TEntity : class =>
            BuildSelector<TEntity>(controller, new[] { identifier });

        public static SelectableEntitySource<TEntity> SelectEntityByIds<TEntity>(
            this ControllerBase controller,
            params object[] identifiers)
            where TEntity : class =>
            BuildSelector<TEntity>(controller, identifiers);

        private static SelectableEntitySource<TEntity> BuildSelector<TEntity>(
            ControllerBase controller,
            IEnumerable<object> keyArguments)
            where TEntity : class
        {
            var filterExpression = controller.HttpContext.RequestServices
                .GetRequiredService<IExpressionFactory<TEntity>>()
                .CreatePrimaryKeyFilterExpression(keyArguments);
            return controller.SelectEntity(filterExpression);
        }
    }
}
