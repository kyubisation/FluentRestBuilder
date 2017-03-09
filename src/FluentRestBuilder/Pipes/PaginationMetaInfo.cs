// <copyright file="PaginationMetaInfo.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes
{
    public class PaginationMetaInfo
    {
        public PaginationMetaInfo(
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
