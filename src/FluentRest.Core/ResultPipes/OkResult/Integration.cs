// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRest
{
    using System.Threading.Tasks;
    using Core;
    using Core.ResultPipes.OkResult;
    using Microsoft.AspNetCore.Mvc;

    public static partial class Integration
    {
        public static Task<IActionResult> ToOkResult<TInput>(
            this IOutputPipe<TInput> pipe)
            where TInput : class
        {
            var resultPipe = new OkResultPipe<TInput>(pipe);
            return ((IPipe)resultPipe).Execute();
        }
    }
}
