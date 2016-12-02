namespace KyubiCode.FluentRest.MetaModel
{
    using System;

    public class PrimaryKeyMismatchException : Exception
    {
        public PrimaryKeyMismatchException(string message)
            : base(message)
        {
        }
    }
}
