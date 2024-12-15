using Elastic.Clients.Elasticsearch;
using Elastic.Transport;

namespace Elasticsearch.API.Extensions
{
    public static class Elasticsearch
    {
        public static IServiceCollection AddElasticsearchServiceExtension(this IServiceCollection services, IConfiguration configuration)
        {
            string username = configuration.GetSection("Elasticsearch")["Username"]!;
            string password = configuration.GetSection("Elasticsearch")["Password"]!;

            var settings = new ElasticsearchClientSettings(new Uri(configuration.GetSection("Elasticsearch")["Url"]!));
            settings.Authentication(new BasicAuthentication(username, password));

            var client = new ElasticsearchClient(settings);

            services.AddSingleton(client);

            return services;
        }
    }
}
