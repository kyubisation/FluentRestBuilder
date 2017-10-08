// <copyright file="PaginationRequest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators.ClientRequest.Interpreters
{
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

        public override string ToString() => Stringifier.Convert(this);
    }
}
