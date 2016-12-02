namespace KyubiCode.FluentRest.Test.ChainPipes.Deletion
{
    using System.Linq;
    using System.Threading.Tasks;
    using FluentRest.ChainPipes.Deletion;
    using Mocks;
    using Xunit;

    public class EntityDeletionPipeTest : ScopedDbContextTestBase
    {
        [Fact]
        public async Task TestDeletion()
        {
            this.CreateEntities();
            var entity = Entity.Entities.First();
            var resultPipe = MockSourcePipe<Entity>.CreateCompleteChain(
                entity,
                this.ServiceProvider,
                p => new EntityDeletionPipe<Entity>(this.Context, p));
            await resultPipe.Execute();
            using (var context = this.CreateContext())
            {
                Assert.Equal(Entity.Entities.Count - 1, context.Entities.Count());
            }
        }
    }
}
