﻿// <copyright file="ExceptionExtension.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Operators
{
    using System;
    using FluentRestBuilder.Operators.Exceptions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;

    public static class ExceptionExtension
    {
        public static Exception ConvertToValidationExceptionIfConcurrencyException(
            this Exception exception) =>
            exception is DbUpdateConcurrencyException concurrencyException
                ? new ValidationException(StatusCodes.Status409Conflict, concurrencyException.Message)
                : exception;
    }
}
