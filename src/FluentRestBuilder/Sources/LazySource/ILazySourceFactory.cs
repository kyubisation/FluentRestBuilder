// <copyright file="ILazySourceFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Sources.LazySource
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public interface ILazySourceFactory<TOutput>
    {
        OutputPipe<TOutput> Resolve(Func<Task<TOutput>> output, ControllerBase controller);
    }
}
