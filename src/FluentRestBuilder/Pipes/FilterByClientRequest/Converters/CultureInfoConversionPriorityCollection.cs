// <copyright file="CultureInfoConversionPriorityCollection.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest.Converters
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;

    public class CultureInfoConversionPriorityCollection : ICultureInfoConversionPriorityCollection
    {
        public IEnumerator<CultureInfo> GetEnumerator()
        {
            yield return CultureInfo.CurrentUICulture;
            yield return CultureInfo.CurrentCulture;
            yield return CultureInfo.InvariantCulture;
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}