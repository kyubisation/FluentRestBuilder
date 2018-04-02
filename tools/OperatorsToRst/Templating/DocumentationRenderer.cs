// <copyright file="DocumentationRenderer.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace OperatorsToRst.Templating
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;
    using RazorLight;

    public class DocumentationRenderer
    {
        private readonly RazorLightEngine engine;

        public DocumentationRenderer()
        {
            this.engine = new RazorLightEngineBuilder()
                .UseEmbeddedResourcesProject(typeof(DocumentationRenderer))
                .UseMemoryCachingProvider()
                .Build();
        }

        public async Task<string> RenderDocumentation(OperatorDocumentation documentation) =>
            await this.engine.CompileRenderAsync("operator", documentation);

        public async Task<string> RenderIndex(IEnumerable<string> files) =>
            await this.engine.CompileRenderAsync("operators", files);
    }
}
