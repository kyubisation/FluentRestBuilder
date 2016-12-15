// <copyright file="Child.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Common.Mocks
{
    public class Child
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ParentId { get; set; }

        public Parent Parent { get; set; }
    }
}
