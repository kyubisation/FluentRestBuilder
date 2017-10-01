// <copyright file="PaginationRequest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators.ClientRequest.Interpreters
{
    using System.ComponentModel;
    using System.Linq;

    public class PaginationRequest
    {
        public PaginationRequest()
        {
        }

        public PaginationRequest(int? offset, int? limit)
        {
            this.Offset = offset;
            this.Limit = limit;
        }

        public int? Offset { get; set; }

        public int? Limit { get; set; }

        public override string ToString()
        {
            var properties = TypeDescriptor.GetProperties(this)
                .Cast<PropertyDescriptor>()
                .Select(p => $"{p.Name}: {p.GetValue(this)}")
                .Aggregate((current, next) => $"{current}, {next}");
            return $"{nameof(PaginationRequest)} {{{properties}}}";
        }
    }
}
