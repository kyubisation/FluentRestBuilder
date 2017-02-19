// <copyright file="ILinkGenerator.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage.Links
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Implement this interface on a response types that inherits from <see cref="RestEntity"/>
    /// in order to generate the appropriate reference links for the mapping.
    /// </summary>
    /// <typeparam name="TEntity">The original entity type.</typeparam>
    public interface ILinkGenerator<in TEntity>
    {
        /// <summary>
        /// Generate appropriate reference links for the current instance.
        /// </summary>
        /// <param name="urlHelper">The url helper.</param>
        /// <param name="entity">The original entity.</param>
        /// <returns>An <see cref="IEnumerable{NamedLink}"/> of named links.</returns>
        IEnumerable<NamedLink> GenerateLinks(IUrlHelper urlHelper, TEntity entity);
    }
}
