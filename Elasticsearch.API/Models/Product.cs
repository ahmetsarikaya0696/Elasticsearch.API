namespace Elasticsearch.API.Models
{
    public class Product
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public ProductFeature? Feature { get; set; }
    }
}
