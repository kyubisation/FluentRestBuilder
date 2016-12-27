// <copyright file="ISourceFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Sources.Source
{
    using System.Threading.Tasks;

    public interface ISourceFactory<TOutput>
    {
        OutputPipe<TOutput> Resolve(Task<TOutput> output);

        OutputPipe<TOutput> Resolve(TOutput output);
    }
}
