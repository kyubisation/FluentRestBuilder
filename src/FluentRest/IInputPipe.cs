// <copyright file="IInputPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public interface IInputPipe<in TInput> : IPipe
    {
        Task<IActionResult> Execute(TInput input);
    }
}
