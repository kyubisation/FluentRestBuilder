namespace KyubiCode.FluentRest.RestCollectionMutators.Pagination
{
    using Common;

    public interface IRestCollectionPagination<TEntity> : IRestCollectionMutator<TEntity>
    {
        int EntriesPerPageDefaultValue { get; set; }

        int MaxEntriesPerPage { get; set; }

        int ActualPage { get; }

        int ActualEntriesPerPage { get; }
    }
}
