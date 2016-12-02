namespace KyubiCode.FluentRest.ChainPipes.CollectionTransformation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentRest.Common;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using SourcePipes.EntityCollection;
    using Transformers.Hal;

    public class CollectionTransformationPipe<TInput, TOutput> :
        InputPipe<IQueryable<TInput>>,
        IInputPipe<IQueryable<TInput>>,
        IOutputPipe<RestEntityCollection>
    {
        private readonly Func<TInput, TOutput> transformation;
        private readonly IRestCollectionLinkGenerator linkGenerator;
        private IInputPipe<RestEntityCollection> child;
        private RestEntityCollection restEntityCollection;

        public CollectionTransformationPipe(
            Func<TInput, TOutput> transformation,
            IRestCollectionLinkGenerator linkGenerator,
            IOutputPipe<IQueryable<TInput>> parent)
            : base(parent)
        {
            this.transformation = transformation;
            this.linkGenerator = linkGenerator;
        }

        TPipe IOutputPipe<RestEntityCollection>.Attach<TPipe>(TPipe pipe)
        {
            this.child = pipe;
            return pipe;
        }

        async Task<IActionResult> IInputPipe<IQueryable<TInput>>.Execute(IQueryable<TInput> input)
        {
            NoPipeAttachedException.Check(this.child);
            var entities = await input.ToListAsync();

            this.restEntityCollection = new RestEntityCollection();
            this.GenerateEmbeddedEntities(entities);
            this.GenerateLinks();

            return await this.child.Execute(this.restEntityCollection);
        }

        private void GenerateEmbeddedEntities(IEnumerable<TInput> entities)
        {
            var transformedEntities = entities.Select(e => this.transformation(e)).ToList();
            this.restEntityCollection.Embedded.Add("items", transformedEntities);
        }

        private void GenerateLinks()
        {
            var paginationMetaInfo = this.GetItem<PaginationMetaInfo>();
            var links = this.linkGenerator.GenerateLinks(paginationMetaInfo);
            this.restEntityCollection.Links = NamedLink.BuildLinks(links);
        }
    }
}
