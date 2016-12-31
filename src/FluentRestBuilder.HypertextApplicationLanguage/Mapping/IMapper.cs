// <copyright file="IMapper.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage.Mapping
{
    public interface IMapper<in TInput, out TOutput>
    {
        TOutput Map(TInput source);

        IMapper<TInput, TOutput> Embed(string name, object value);
    }
}
