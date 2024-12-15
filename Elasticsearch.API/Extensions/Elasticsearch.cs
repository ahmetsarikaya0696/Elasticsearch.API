using Elasticsearch.Net;
using Nest;

namespace Elasticsearch.API.Extensions
{
    public static class Elasticsearch
    {
        public static IServiceCollection AddElasticsearchServiceExtension(this IServiceCollection services, IConfiguration configuration)
        {
            var pool = new SingleNodeConnectionPool(new Uri(configuration.GetSection("Elasticsearch")["Url"]!));
            var settings = new ConnectionSettings(pool);

            settings.BasicAuthentication(configuration.GetSection("Elasticsearch")["Username"]!, configuration.GetSection("Elasticsearch")["Password"]!);

            var client = new ElasticClient(settings);
            services.AddSingleton(client);

            return services;
        }
    }
}
