using Newtonsoft.Json;

namespace DependencyInjection.Sample.TightlyCoupled.Core
{
    public record Product
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("unit_price")]
        public decimal UnitPrice { get; set; }
    }
}
