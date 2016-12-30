// <copyright file="NotSupportedEntriesPerPageException.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.PaginationByClientRequest.Exceptions
{
    public class NotSupportedEntriesPerPageException : PaginationException
    {
        public NotSupportedEntriesPerPageException(string entriesPerPage)
            : base($"Entries per page value '{entriesPerPage}' is not supported!")
        {
        }
    }
}
