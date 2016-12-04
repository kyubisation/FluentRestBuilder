// <copyright file="IItemProvider.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core
{
    using System;

    public interface IItemProvider
    {
        object GetItem(Type itemType);
    }
}
