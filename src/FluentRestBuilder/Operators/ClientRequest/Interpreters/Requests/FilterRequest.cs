// <copyright file="FilterRequest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators.ClientRequest.Interpreters.Requests
{
    public class FilterRequest
    {
        public FilterRequest(
            string originalProperty, string property, FilterType filterType, string filter)
        {
            this.OriginalProperty = originalProperty;
            this.Property = property;
            this.FilterType = filterType;
            this.Filter = filter;
        }

        public FilterRequest(string property, FilterType filterType, string filter)
            : this(property, property, filterType, filter)
        {
        }

        public string OriginalProperty { get; }

        public string Property { get; }

        public FilterType FilterType { get; }

        public string Filter { get; }

        public override string ToString() => Stringifier.Convert(this);
    }
}