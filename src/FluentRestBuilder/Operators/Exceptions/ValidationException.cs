// <copyright file="ValidationException.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators.Exceptions
{
    using System;

    public class ValidationException : Exception
    {
        public ValidationException(int statusCode, object error = null)
        {
            this.StatusCode = statusCode;
            this.Error = error;
        }

        public int StatusCode { get; }

        public object Error { get; }
    }
}
