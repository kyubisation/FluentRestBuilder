namespace KyubiCode.FluentRest.Transformers.Hal
{
    public class LinkToSelf : NamedLink
    {
        public LinkToSelf(Link link)
            : base(Link.Self, link)
        {
        }
    }
}
