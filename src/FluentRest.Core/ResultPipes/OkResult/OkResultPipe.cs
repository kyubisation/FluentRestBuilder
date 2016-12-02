namespace KyubiCode.FluentRest.ResultPipes.OkResult
{
    using Common;
    using Microsoft.AspNetCore.Mvc;

    public class OkResultPipe<TInput> : ResultPipe<TInput>
        where TInput : class
    {
        public OkResultPipe(IOutputPipe<TInput> parent)
            : base(parent)
        {
        }

        protected override IActionResult CreateResult(TInput source) =>
            new OkObjectResult(source);
    }
}
