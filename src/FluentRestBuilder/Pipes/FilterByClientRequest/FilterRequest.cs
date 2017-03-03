// <copyright file="FilterRequest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest
{
    using System.ComponentModel;
    using System.Linq;

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

        public override string ToString()
        {
            var properties = TypeDescriptor.GetProperties(this)
                .Cast<PropertyDescriptor>()
                .Select(p => $"{p.Name}: {p.GetValue(this)}")
                .Aggregate((current, next) => $"{current}, {next}");
            return $"{nameof(FilterRequest)} {{{properties}}}";
        }
    }
}