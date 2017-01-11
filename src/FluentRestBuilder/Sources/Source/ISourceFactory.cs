// <copyright file="ISourceFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Sources.Source
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public interface ISourceFactory<TOutput>
    {
        OutputPipe<TOutput> Resolve(Task<TOutput> output, ControllerBase controller);
    }
}
