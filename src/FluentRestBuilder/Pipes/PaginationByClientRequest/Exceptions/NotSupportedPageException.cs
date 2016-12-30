// <copyright file="NotSupportedPageException.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.PaginationByClientRequest.Exceptions
{
    public class NotSupportedPageException : PaginationException
    {
        public NotSupportedPageException(string page)
            : base($"Page value '{page}' is not supported!")
        {
        }
    }
}
