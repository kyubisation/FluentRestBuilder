// <copyright file="MaxEntriesPerPageExceededException.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.PaginationByClientRequest.Exceptions
{
    public class MaxEntriesPerPageExceededException : PaginationException
    {
        public MaxEntriesPerPageExceededException(
            int requestedEntriesPerPage, int maxEntriesPerPage)
            : base($"Requested entries per page '{requestedEntriesPerPage}'" +
                   $" exceeded max entries per page '{maxEntriesPerPage}'")
        {
        }
    }
}
