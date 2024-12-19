using System.Text.Json.Serialization;

namespace Elasticsearch.Web.Models
{
    public class Event
    {
        [JsonPropertyName("dataset")]
        public string Dataset { get; set; } = default!;
    }
}
