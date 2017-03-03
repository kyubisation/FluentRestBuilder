// <copyright file="PaginationRequest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.PaginationByClientRequest
{
    using System.ComponentModel;
    using System.Linq;

    public class PaginationRequest
    {
        public PaginationRequest(int? page, int? entriesPerPage)
        {
            this.Page = page;
            this.EntriesPerPage = entriesPerPage;
        }

        public int? Page { get; set; }

        public int? EntriesPerPage { get; set; }

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
