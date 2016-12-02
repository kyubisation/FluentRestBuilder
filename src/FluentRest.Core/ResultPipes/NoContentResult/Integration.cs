// ReSharper disable once CheckNamespace
namespace KyubiCode.FluentRest
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using ResultPipes.NoContentResult;

    public static partial class Integration
    {
        public static Task<IActionResult> ToNoContentResult<TInput>(
            this IOutputPipe<TInput> pipe)
            where TInput : class
        {
            var resultPipe = new NoContentResultPipe<TInput>(pipe);
            return ((IPipe)resultPipe).Execute();
        }
    }
}
