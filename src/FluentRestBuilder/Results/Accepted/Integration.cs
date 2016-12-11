// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRest
{
    using System.Threading.Tasks;
    using Core;
    using Core.Results.Accepted;
    using Microsoft.AspNetCore.Mvc;

    public static partial class Integration
    {
        public static Task<IActionResult> ToAcceptedResult<TInput>(
            this IOutputPipe<TInput> pipe)
            where TInput : class
        {
            IPipe resultPipe = new AcceptedResultPipe<TInput>(pipe);
            return resultPipe.Execute();
        }
    }
}
