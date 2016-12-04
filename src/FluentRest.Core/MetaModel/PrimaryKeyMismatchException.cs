// <copyright file="PrimaryKeyMismatchException.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.MetaModel
{
    using System;

    public class PrimaryKeyMismatchException : Exception
    {
        public PrimaryKeyMismatchException(string message)
            : base(message)
        {
        }
    }
}
