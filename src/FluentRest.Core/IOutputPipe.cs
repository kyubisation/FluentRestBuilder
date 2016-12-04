// <copyright file="IOutputPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core
{
    public interface IOutputPipe<out TOutput> : IPipe
    {
        TPipe Attach<TPipe>(TPipe pipe)
            where TPipe : IInputPipe<TOutput>;
    }
}
