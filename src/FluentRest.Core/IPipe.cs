namespace KyubiCode.FluentRest
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public interface IPipe : IServiceProvider, IItemProvider
    {
        Task<IActionResult> Execute();
    }
}
