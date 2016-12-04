﻿// <copyright file="ITransformerFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.Transformers
{
    public interface ITransformerFactory
    {
        ITransformerFactory<TInput> Resolve<TInput>();
    }

    public interface ITransformerFactory<in TInput>
    {
        ITransformer<TInput, TOutput> Resolve<TOutput>();
    }
}
