namespace KyubiCode.FluentRest.MetaModel
{
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore.Metadata;

    // ReSharper disable once UnusedTypeParameter
    public interface IModelContainer<TEntity>
    {
        IEntityType EntityType { get; }

        IKey PrimaryKey { get; }

        IReadOnlyCollection<IProperty> Properties { get; }

        IReadOnlyCollection<INavigation> Navigations { get; }
    }
}
