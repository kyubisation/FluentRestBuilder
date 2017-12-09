// <copyright file="CultureInfoExtensions.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Mocks
{
    using System.Globalization;

    public static class CultureInfoExtensions
    {
        public static CultureInfo AssignAsCurrentUiCulture(this CultureInfo cultureInfo)
        {
            CultureInfo.CurrentUICulture = cultureInfo;
            return cultureInfo;
        }
    }
}
