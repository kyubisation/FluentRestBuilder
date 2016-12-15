// <copyright file="Parent.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Common.Mocks
{
    using System.Collections.Generic;

    public class Parent
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Child> Children { get; set; }
    }
}
