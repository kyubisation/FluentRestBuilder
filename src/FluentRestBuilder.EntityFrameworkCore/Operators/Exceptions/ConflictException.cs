// <copyright file="ConflictException.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Operators.Exceptions
{
    using FluentRestBuilder.Operators.Exceptions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;

    public class ConflictException : ValidationException
    {
        public ConflictException(object error = null, DbUpdateConcurrencyException exception = null)
            : base(StatusCodes.Status409Conflict, error, exception)
        {
        }

        public ConflictException(DbUpdateConcurrencyException exception)
            : this(exception.Message, exception)
        {
        }
    }
}
