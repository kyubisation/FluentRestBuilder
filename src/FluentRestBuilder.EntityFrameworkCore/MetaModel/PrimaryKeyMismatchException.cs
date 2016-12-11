// <copyright file="PrimaryKeyMismatchException.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.MetaModel
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
