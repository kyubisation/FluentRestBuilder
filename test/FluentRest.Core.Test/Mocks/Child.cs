namespace KyubiCode.FluentRest.Test.Mocks
{
    public class Child
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ParentId { get; set; }

        public Parent Parent { get; set; }
    }
}
