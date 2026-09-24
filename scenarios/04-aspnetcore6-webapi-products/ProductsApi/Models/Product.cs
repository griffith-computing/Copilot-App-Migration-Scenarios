using Newtonsoft.Json;

namespace ProductsApi.Models
{
    /// <summary>
    /// Same Products domain shape used across the other migration scenarios
    /// in this repo. The JsonProperty attributes are a deliberate holdover
    /// from a Json.NET-based DTO layer -- functionally fine on ASP.NET Core
    /// 6 (via the NewtonsoftJson formatter package), but worth flagging
    /// during a migration: does this API still need Json.NET compatibility,
    /// or can it move to plain System.Text.Json attributes/records?
    /// </summary>
    public class Product
    {
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("category")]
        public string Category { get; set; } = string.Empty;

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("inStock")]
        public bool InStock { get; set; }

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }
    }
}
