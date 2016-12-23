// <copyright file="IMapperFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Mapping
{
    public interface IMapperFactory
    {
        IMapperFactory<TInput> Resolve<TInput>();
    }

    public interface IMapperFactory<in TInput>
    {
        IMapper<TInput, TOutput> Resolve<TOutput>();
    }
}
