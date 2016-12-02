// ReSharper disable once CheckNamespace
namespace KyubiCode.FluentRest
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using ResultPipes.OkResult;

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
