// <copyright file="IReflectionMapper.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage.Mapping.Reflection
{
    public interface IReflectionMapper<in TInput, out TOutput>
    {
        TOutput Map(TInput input);
    }
}
