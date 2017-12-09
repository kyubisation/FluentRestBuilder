// <copyright file="PaginationInfo.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators.ClientRequest
{
    public class PaginationInfo
    {
        public PaginationInfo(
            int total,
            int offset,
            int limit)
        {
            this.Total = total;
            this.Offset = offset;
            this.Limit = limit;
        }

        public int Total { get; }

        public int Offset { get; }

        public int Limit { get; }
    }
}
