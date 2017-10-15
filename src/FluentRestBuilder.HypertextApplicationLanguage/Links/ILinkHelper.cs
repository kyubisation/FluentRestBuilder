// <copyright file="ILinkHelper.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage.Links
{
    using System.Collections.Generic;

    public interface ILinkHelper
    {
        string CurrentUrl();

        string ModifyCurrentUrl(IDictionary<string, string> modifications);
    }
}
