namespace KyubiCode.FluentRest.RestCollectionMutators
{
    public interface IQueryArgumentKeys
    {
        string Page { get; }

        string EntriesPerPage { get; }

        string Filter { get; }

        string OrderBy { get; }

        string Search { get; }
    }
}
