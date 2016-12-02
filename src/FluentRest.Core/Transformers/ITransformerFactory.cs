namespace KyubiCode.FluentRest.Transformers
{
    public interface ITransformerFactory
    {
        ITransformerFactory<TInput> Resolve<TInput>();
    }

    public interface ITransformerFactory<in TInput>
    {
        ITransformer<TInput, TOutput> Resolve<TOutput>();
    }
}
