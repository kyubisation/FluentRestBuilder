// <copyright file="ErrorResult.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes
{
    using System;

    public class ErrorResult
    {
        public ErrorResult(Exception exception)
        {
            this.Message = exception.Message;
        }

        public string Message { get; set; }
    }
}
