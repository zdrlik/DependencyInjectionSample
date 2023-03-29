namespace DependencyInjection.Sample.LooselyCoupled.Core
{
    public record Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
