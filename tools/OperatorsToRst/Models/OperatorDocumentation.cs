// <copyright file="OperatorDocumentation.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace OperatorsToRst.Models
{
    using System.Collections.Generic;

    public class OperatorDocumentation
    {
        public OperatorDocumentation(string name, List<OperatorVariantDocumentation> variants)
        {
            this.Name = name;
            this.Variants = variants;
        }

        public string Name { get; set; }

        public List<OperatorVariantDocumentation> Variants { get; set; }
    }
}
