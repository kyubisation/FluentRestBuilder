// <copyright file="FilterRequest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest
{
    using System.ComponentModel;
    using System.Linq;

    public class FilterRequest
    {
        public FilterRequest(string property, FilterType filterType, string filter)
        {
            this.Property = property;
            this.FilterType = filterType;
            this.Filter = filter;
        }

        public string Property { get; }

        public FilterType FilterType { get; }

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