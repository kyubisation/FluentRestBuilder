// ReSharper disable once CheckNamespace
namespace KyubiCode.FluentRest
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using ResultPipes.CreatedEntityResult;

    public static partial class Integration
    {
        public static Task<IActionResult> ToCreatedAtRouteResult<TInput, TLookup>(
            this IOutputPipe<TInput> pipe,
            string routeName,
            Func<TLookup, object> routeValuesGenerator)
            where TInput : class
        {
            var createdEntityResultPipe = new CreatedEntityResultPipe<TInput>(
                (IPipe p) => routeValuesGenerator(pipe.GetItem<TLookup>()),
                routeName,
                pipe);
            return ((IPipe)createdEntityResultPipe).Execute();
        }

        public static Task<IActionResult> ToCreatedAtRouteResult<TInput>(
            this IOutputPipe<TInput> pipe,
            string routeName,
            Func<TInput, object> routeValuesGenerator)
            where TInput : class
        {
            var createdEntityResultPipe = new CreatedEntityResultPipe<TInput>(
                routeValuesGenerator,
                routeName,
                pipe);
            return ((IPipe)createdEntityResultPipe).Execute();
        }
    }
}
