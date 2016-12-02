namespace KyubiCode.FluentRest.Test.Mocks
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public class MockActionResult : IActionResult
    {
        public Task ExecuteResultAsync(ActionContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
