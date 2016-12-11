// <copyright file="IPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public interface IPipe : IServiceProvider
    {
        Task<IActionResult> Execute();
    }
}
