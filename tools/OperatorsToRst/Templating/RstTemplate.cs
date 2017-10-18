// <copyright file="RstTemplate.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace OperatorsToRst.Templating
{
    using System.IO;
    using System.Reflection;
    using DotLiquid;
    using DotLiquid.NamingConventions;

    public class RstTemplate
    {
        private readonly Template template;

        static RstTemplate()
        {
            Template.NamingConvention = new CSharpNamingConvention();
            Template.RegisterFilter(typeof(TextFilters));
        }

        public RstTemplate(string template)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var embeddedFile = $"{nameof(OperatorsToRst)}.{template}.csrst";
            using (var stream = assembly.GetManifestResourceStream(embeddedFile))
            using (var reader = new StreamReader(stream))
            {
                this.template = Template.Parse(reader.ReadToEnd());
            }
        }

        public string Render<TValue>(TValue value) =>
            this.template.Render(Hash.FromAnonymousObject(value));
    }
}
