// <copyright file="ISourcePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.Sources.Source
{
    using System.Threading.Tasks;

    public interface ISourcePipeFactory<TOutput>
    {
        SourcePipe<TOutput> Resolve(Task<TOutput> output);

        SourcePipe<TOutput> Resolve(TOutput output);
    }
}
