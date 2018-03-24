// <copyright file="MockDbUpdateConcurrencyException.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Mocks
{
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Update;

    public class MockDbUpdateConcurrencyException : DbUpdateConcurrencyException
    {
        public MockDbUpdateConcurrencyException()
            : base(nameof(MockDbUpdateConcurrencyException), MockEntries())
        {
        }

        private static IReadOnlyList<IUpdateEntry> MockEntries()
        {
            return new List<IUpdateEntry> { new MockUpdateEntry() };
        }

        private class MockUpdateEntry : IUpdateEntry
        {
            public IEntityType EntityType => null;

            public EntityState EntityState => EntityState.Unchanged;

            public bool IsModified(IProperty property) => false;

            public bool HasTemporaryValue(IProperty property) => false;

            public bool IsStoreGenerated(IProperty property) => false;

            public object GetCurrentValue(IPropertyBase propertyBase) => null;

            public object GetOriginalValue(IPropertyBase propertyBase) => null;

            public TProperty GetCurrentValue<TProperty>(IPropertyBase propertyBase) =>
                default(TProperty);

            public TProperty GetOriginalValue<TProperty>(IProperty property) => default(TProperty);

            public void SetCurrentValue(IPropertyBase propertyBase, object value)
            {
            }

            public EntityEntry ToEntityEntry() => null;
        }
    }
}
