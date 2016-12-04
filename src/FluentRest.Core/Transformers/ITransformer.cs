// <copyright file="ITransformer.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.Transformers
{
    public interface ITransformer<in TInput, out TOutput>
    {
        TOutput Transform(TInput source);

        ITransformer<TInput, TOutput> Embed(string name, object value);
    }
}
