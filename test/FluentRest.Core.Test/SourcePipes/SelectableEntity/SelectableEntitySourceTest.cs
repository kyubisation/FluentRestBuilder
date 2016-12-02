namespace KyubiCode.FluentRest.Test.SourcePipes.SelectableEntity
{
    using System.Linq;
    using System.Threading.Tasks;
    using Common;
    using FluentRest.SourcePipes.SelectableEntity;
    using Mocks;
    using Xunit;

    public class SelectableEntitySourceTest : ScopedDbContextTestBase
    {
        private readonly Entity entity;

        public SelectableEntitySourceTest()
        {
            this.entity = Entity.Entities.First();
        }

        [Fact]
        public async Task TestNoAttachedChild()
        {
            this.CreateEntities();
            var source = this.CreateSource();
            await Assert.ThrowsAsync<NoPipeAttachedException>(
                async () => await ((IPipe)source).Execute());
        }

        [Fact]
        public async Task TestEntityDoesNotExist()
        {
            var source = this.CreateSource();
            var resultPipe = new MockResultPipe<Entity>(source);

            await resultPipe.Execute();
            Assert.Null(resultPipe.Input);
        }

        [Fact]
        public async Task TestNormalSelection()
        {
            this.CreateEntities();
            var source = this.CreateSource();
            var resultPipe = new MockResultPipe<Entity>(source);

            await resultPipe.Execute();
            Assert.Equal(this.entity.Id, resultPipe.Input.Id);
        }

        private SelectableEntitySource<Entity> CreateSource()
        {
            return new SelectableEntitySource<Entity>(
                e => e.Id == this.entity.Id,
                this.ResolveScoped<IQueryableFactory<Entity>>().Queryable,
                this.ServiceProvider);
        }
    }
}
