 // <copyright file="Integration.cs" company="Kyubisation">
 // Copyright (c) Kyubisation. All rights reserved.
 // </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRest
{
    using System;
    using System.Threading.Tasks;
    using Core;
    using Core.Pipes.Actions;

    public static partial class Integration
    {
        public static ActionPipe<TInput> Do<TInput>(
            this IOutputPipe<TInput> pipe, Action<TInput> action)
            where TInput : class =>
            new ActionPipe<TInput>(action, pipe);

        public static ActionPipe<TInput> Do<TInput>(
            this IOutputPipe<TInput> pipe, Func<TInput, Task> action)
            where TInput : class =>
            new ActionPipe<TInput>(action, pipe);
    }
}
