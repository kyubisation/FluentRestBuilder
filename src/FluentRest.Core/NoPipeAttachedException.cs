namespace KyubiCode.FluentRest
{
    using System;

    public class NoPipeAttachedException : InvalidOperationException
    {
        public NoPipeAttachedException()
            : base("Must have a pipe attached!")
        {
        }

        public static void Check(object check)
        {
            if (check == null)
            {
                throw new NoPipeAttachedException();
            }
        }
    }
}
