// <copyright file="PrimaryKeyMismatchException.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.MetaModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore.Metadata;

    public class PrimaryKeyMismatchException : Exception
    {
        public PrimaryKeyMismatchException(IKey key, IReadOnlyCollection<Type> valueTypes)
            : base(BuildMessage(key, valueTypes))
        {
        }

        private static string BuildMessage(IKey key, IReadOnlyCollection<Type> valueTypes)
        {
            var primaryKeySequence = key.Properties
                .Select(p => p.ClrType.Name)
                .Aggregate((current, next) => $"{current}, {next}");
            if (valueTypes.Count == 0)
            {
                return $"Expected {primaryKeySequence} but received no keys!";
            }

            var valueTypeSequence = valueTypes.Select(k => k.Name)
                .Aggregate((current, next) => $"{current}, {next}");
            return $"Expected {primaryKeySequence} but received {valueTypeSequence}!";
        }
    }
}
