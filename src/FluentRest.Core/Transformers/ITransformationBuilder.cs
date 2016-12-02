namespace KyubiCode.FluentRest.Transformers
{
    using System;

    public interface ITransformationBuilder<TInput>
    {
        ITransformationBuilder<TInput> Embed<TEmbeddedResource, TTransformedResource>(
            string name,
            Func<TInput, TEmbeddedResource> resourceSelector,
            Func<
                    ITransformerFactory<TEmbeddedResource>,
                    ITransformer<TEmbeddedResource, TTransformedResource>>
                transformerSelector);

        Func<TInput, TOutput> UseTransformer<TOutput>(
            Func<ITransformerFactory<TInput>, ITransformer<TInput, TOutput>> selection);
    }
}