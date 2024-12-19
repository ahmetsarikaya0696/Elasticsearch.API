using Elasticsearch.Web.Models;

namespace Elasticsearch.Web.ViewModels
{
    public class ECommerceViewModel
    {
        public string Id { get; set; } = null!;

        public string? Category { get; set; }

        public string Currency { get; set; } = default!;

        public DateTime? CustomerBirthDate { get; set; }

        public string CustomerFirstName { get; set; } = default!;

        public string CustomerFullName { get; set; } = default!;

        public string CustomerGender { get; set; } = default!;

        public int CustomerId { get; set; }

        public string CustomerLastName { get; set; } = default!;

        public string CustomerPhone { get; set; } = default!;

        public string DayOfWeek { get; set; } = default!;

        public int? DayOfWeekI { get; set; }

        public string Email { get; set; } = default!;

        public Event Event { get; set; } = default!;

        public GeoIp GeoIp { get; set; } = default!;

        public List<string> Manufacturer { get; set; } = [];

        public string? OrderDate { get; set; }

        public int OrderId { get; set; }

        public List<Product> Products { get; set; } = [];

        public List<string> Sku { get; set; } = [];

        public float? TaxfulTotalPrice { get; set; }

        public float? TaxlessTotalPrice { get; set; }

        public int? TotalQuantity { get; set; }

        public int? TotalUniqueProducts { get; set; }

        public string Type { get; set; } = default!;

        public string User { get; set; } = default!;
    }
}
