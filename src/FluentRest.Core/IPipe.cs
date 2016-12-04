// <copyright file="IPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public interface IPipe : IServiceProvider, IItemProvider
    {
        Task<IActionResult> Execute();
    }
}
