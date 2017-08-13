// <copyright file="FilterRequiresControllerException.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Filters
{
    using System;

    public class FilterRequiresControllerException : Exception
    {
        public FilterRequiresControllerException()
            : base("This filter can only be used with an MVC controller or base controller (or descendants).")
        {
        }
    }
}
