namespace KyubiCode.FluentRest.ChainPipes.Validation
{
    using System;
    using System.Threading.Tasks;

    public class ValidationPipe<TInput> : Common.ValidationPipe<TInput>
        where TInput : class
    {
        private readonly Func<Task<bool>> invalidCheck;

        public ValidationPipe(
            Func<Task<bool>> invalidCheck,
            int statusCode,
            object error,
            IOutputPipe<TInput> parent)
            : base(statusCode, error, parent)
        {
            this.invalidCheck = invalidCheck;
        }

        public ValidationPipe(
            Func<bool> invalidCheck,
            int statusCode,
            object error,
            IOutputPipe<TInput> parent)
            : this(() => Task.FromResult(invalidCheck()), statusCode, error, parent)
        {
        }

        protected override Task<bool> IsInvalid(TInput entity) => this.invalidCheck();
    }
}
