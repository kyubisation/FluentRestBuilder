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
#if NET452
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr-FR");
#else
            CultureInfo.CurrentUICulture = new CultureInfo("fr-FR");
#endif
            return cultureInfo;
        }
    }
}
