using Elastic.Clients.Elasticsearch;
using System.Text.Json.Serialization;

namespace Elasticsearch.Web.Models
{
    public class GeoIp
    {
        [JsonPropertyName("city_name")]
        public string CityName { get; set; } = default!;

        [JsonPropertyName("continent_name")]
        public string ContinentName { get; set; } = default!;

        [JsonPropertyName("country_iso_code")]
        public string CountryIsoCode { get; set; } = default!;

        [JsonPropertyName("location")]
        public GeoLocation Location { get; set; } = default!;

        [JsonPropertyName("region_name")]
        public string RegionName { get; set; } = default!;
    }

}
