namespace KyubiCode.FluentRest.Transformers
{
    public interface ITransformer<in TInput, out TOutput>
    {
        TOutput Transform(TInput source);

        ITransformer<TInput, TOutput> Embed(string name, object value);
    }
}
