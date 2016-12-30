// <copyright file="FilterInterpreterException.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest.Exceptions
{
    public class FilterInterpreterException : FilterException
    {
        public FilterInterpreterException(string message)
            : base(message)
        {
        }
    }
}
