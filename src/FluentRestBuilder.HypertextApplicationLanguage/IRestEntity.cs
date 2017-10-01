// <copyright file="IRestEntity.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable InconsistentNaming
namespace FluentRestBuilder.HypertextApplicationLanguage
{
    using System.Collections.Generic;

#pragma warning disable SA1300 // Element should begin with upper-case letter
    public interface IRestEntity
    {
        IDictionary<string, object> _links { get; set; }

        IDictionary<string, object> _embedded { get; set; }
    }
#pragma warning restore SA1300 // Element should begin with upper-case letter
}