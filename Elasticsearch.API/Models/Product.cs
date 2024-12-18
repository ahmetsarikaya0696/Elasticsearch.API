using System.Text.Json.Serialization;

namespace Elasticsearch.API.Models
{
    public class Product
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; } = default!;

        [JsonPropertyName("base_price")]
        public float? BasePrice { get; set; }

        [JsonPropertyName("base_unit_price")]
        public float? BaseUnitPrice { get; set; }

        [JsonPropertyName("category")]
        public string Category { get; set; } = default!;

        [JsonPropertyName("created_on")]
        public DateTime? CreatedOn { get; set; }

        [JsonPropertyName("discount_amount")]
        public float? DiscountAmount { get; set; }

        [JsonPropertyName("discount_percentage")]
        public float? DiscountPercentage { get; set; }

        [JsonPropertyName("manufacturer")]
        public string Manufacturer { get; set; } = default!;

        [JsonPropertyName("min_price")]
        public float? MinPrice { get; set; }

        [JsonPropertyName("price")]
        public float? Price { get; set; }

        [JsonPropertyName("product_id")]
        public long? ProductId { get; set; }

        [JsonPropertyName("product_name")]
        public string ProductName { get; set; } = default!;

        [JsonPropertyName("quantity")]
        public int? Quantity { get; set; }

        [JsonPropertyName("sku")]
        public string Sku { get; set; } = default!;

        [JsonPropertyName("tax_amount")]
        public float? TaxAmount { get; set; }

        [JsonPropertyName("taxful_price")]
        public float? TaxfulPrice { get; set; }

        [JsonPropertyName("taxless_price")]
        public float? TaxlessPrice { get; set; }

        [JsonPropertyName("unit_discount_amount")]
        public float? UnitDiscountAmount { get; set; }
    }
}
