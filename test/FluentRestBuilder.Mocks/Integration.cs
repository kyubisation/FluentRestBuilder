// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Mocks
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public static class Integration
    {
        public static MockResult<TInput> ToMockResultPipe<TInput>(this IOutputPipe<TInput> pipe)
            where TInput : class =>
            new MockResult<TInput>(pipe);

        public static TOutput GetObjectResultOrDefault<TOutput>(this IActionResult actionResult)
            where TOutput : class
        {
            var objectResult = actionResult as ObjectResult;
            return objectResult?.Value as TOutput;
        }

        public static async Task<TOutput> GetObjectResultOrDefault<TOutput>(
            this Task<IActionResult> actionResult)
            where TOutput : class =>
            (await actionResult).GetObjectResultOrDefault<TOutput>();

        public static async Task<TOutput> ToObjectResultOrDefault<TOutput>(
            this IOutputPipe<TOutput> pipe)
            where TOutput : class =>
            await pipe.ToMockResultPipe()
                .Execute()
                .GetObjectResultOrDefault<TOutput>();
    }
}
