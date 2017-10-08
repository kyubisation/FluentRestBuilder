// <copyright file="Stringifier.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder
{
    using System.ComponentModel;
    using System.Linq;

    public static class Stringifier
    {
        public static string Convert<TType>(TType value)
        {
            var properties = TypeDescriptor.GetProperties(value)
                .Cast<PropertyDescriptor>()
                .Select(p => $"{p.Name}: {p.GetValue(value)}")
                .Aggregate((current, next) => $"{current}, {next}");
            return $"{value.GetType().Name} {{{properties}}}";
        }
    }
}
