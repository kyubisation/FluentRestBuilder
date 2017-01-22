// <copyright file="TargetResponse.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage.Test.Mocks
{
    public class TargetResponse : RestEntity
    {
        public int Id { get; set; }

        public int Id2 { get; set; }

        public int? Id3 { get; set; }

        public int? Id4 { get; set; }

        public object Name { get; set; }
    }
}
