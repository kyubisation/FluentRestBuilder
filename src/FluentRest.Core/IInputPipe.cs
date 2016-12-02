namespace KyubiCode.FluentRest
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public interface IInputPipe<in TInput> : IPipe
    {
        Task<IActionResult> Execute(TInput input);
    }
}
