namespace KyubiCode.FluentRest.SourcePipes.EntityCollection
{
    public interface IEntityCollectionSourceFactory<TEntity>
        where TEntity : class
    {
        EntityCollectionSource<TEntity> Resolve();
    }
}