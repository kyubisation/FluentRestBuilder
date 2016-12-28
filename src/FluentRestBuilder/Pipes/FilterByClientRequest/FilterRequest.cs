// <copyright file="FilterRequest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest
{
    public class FilterRequest
    {
        public FilterRequest(
            string originalProperty, string property, FilterType type, string filter)
        {
            this.OriginalProperty = originalProperty;
            this.Property = property;
            this.Type = type;
            this.Filter = filter;
        }

        public FilterRequest(string property, FilterType type, string filter)
            : this(property, property, type, filter)
        {
        }

        public string OriginalProperty { get; }

        public string Property { get; }

        public FilterType Type { get; }

        public string Filter { get; }
    }
}
