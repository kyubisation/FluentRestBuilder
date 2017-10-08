// <copyright file="FilterRequest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators.ClientRequest.Interpreters
{
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

        public override string ToString() => Stringifier.Convert(this);
    }
}