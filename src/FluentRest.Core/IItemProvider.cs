namespace KyubiCode.FluentRest
{
    using System;

    public interface IItemProvider
    {
        object GetItem(Type itemType);
    }
}
