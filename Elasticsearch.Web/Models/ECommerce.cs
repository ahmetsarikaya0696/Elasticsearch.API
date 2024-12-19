using System.Text.Json.Serialization;

namespace Elasticsearch.Web.Models
{
    public class ECommerce
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; } = null!;

        [JsonPropertyName("category")]
        public List<string> Category { get; set; } = [];

        [JsonPropertyName("currency")]
        public string Currency { get; set; } = default!;

        [JsonPropertyName("customer_birth_date")]
        public DateTime? CustomerBirthDate { get; set; }

        [JsonPropertyName("customer_first_name")]
        public string CustomerFirstName { get; set; } = default!;

        [JsonPropertyName("customer_full_name")]
        public string CustomerFullName { get; set; } = default!;

        [JsonPropertyName("customer_gender")]
        public string CustomerGender { get; set; } = default!;

        [JsonPropertyName("customer_id")]
        public int CustomerId { get; set; }

        [JsonPropertyName("customer_last_name")]
        public string CustomerLastName { get; set; } = default!;

        [JsonPropertyName("customer_phone")]
        public string CustomerPhone { get; set; } = default!;

        [JsonPropertyName("day_of_week")]
        public string DayOfWeek { get; set; } = default!;

        [JsonPropertyName("day_of_week_i")]
        public int? DayOfWeekI { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; } = default!;

        [JsonPropertyName("event")]
        public Event Event { get; set; } = default!;

        [JsonPropertyName("geoip")]
        public GeoIp GeoIp { get; set; } = default!;

        [JsonPropertyName("manufacturer")]
        public List<string> Manufacturer { get; set; } = [];

        [JsonPropertyName("order_date")]
        public DateTime? OrderDate { get; set; }

        [JsonPropertyName("order_id")]
        public int OrderId { get; set; }

        [JsonPropertyName("products")]
        public List<Product> Products { get; set; } = [];

        [JsonPropertyName("sku")]
        public List<string> Sku { get; set; } = [];

        [JsonPropertyName("taxful_total_price")]
        public float? TaxfulTotalPrice { get; set; }

        [JsonPropertyName("taxless_total_price")]
        public float? TaxlessTotalPrice { get; set; }

        [JsonPropertyName("total_quantity")]
        public int? TotalQuantity { get; set; }

        [JsonPropertyName("total_unique_products")]
        public int? TotalUniqueProducts { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; } = default!;

        [JsonPropertyName("user")]
        public string User { get; set; } = default!;
    }
}
