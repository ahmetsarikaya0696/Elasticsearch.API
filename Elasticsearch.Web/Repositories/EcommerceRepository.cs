using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Elasticsearch.Web.Models;
using Elasticsearch.Web.ViewModels;

namespace Elasticsearch.Web.Repositories
{
    public class EcommerceRepository(ElasticsearchClient elasticsearchClient)
    {
        private const string indexName = "kibana_sample_data_ecommerce";

        public async Task<(List<ECommerce> list, long count)> SearchAsync(ECommerceSearchViewModel eCommerceSearchViewModel, int page, int pageSize)
        {
            List<Action<QueryDescriptor<ECommerce>>> queries = [];

            if (eCommerceSearchViewModel is null)
            {
                Action<QueryDescriptor<ECommerce>> query = q => q.MatchAll(m => { });
                queries.Add(query);

                return await CalculateResultSetAsync(elasticsearchClient, page, pageSize, queries);
            }

            if (!string.IsNullOrEmpty(eCommerceSearchViewModel.Category))
            {
                Action<QueryDescriptor<ECommerce>> query = q => q.Match(q => q
                                                                    .Field(f => f.Category)
                                                                    .Query(eCommerceSearchViewModel.Category!)
                                                                );

                queries.Add(query);
            }

            if (!string.IsNullOrEmpty(eCommerceSearchViewModel.CustomerFullName))
            {
                Action<QueryDescriptor<ECommerce>> query = q => q.Match(q => q
                                                                    .Field(f => f.CustomerFullName)
                                                                    .Query(eCommerceSearchViewModel.CustomerFullName!)
                                                                );

                queries.Add(query);
            }

            if (eCommerceSearchViewModel.OrderDateStart.HasValue)
            {
                Action<QueryDescriptor<ECommerce>> query = q => q.Range(r => r
                                                                            .DateRange(dr => dr
                                                                                .Field(f => f.OrderDate)
                                                                                .Gte(eCommerceSearchViewModel.OrderDateStart.Value)
                                                                            )
                                                                        );

                queries.Add(query);
            }

            if (eCommerceSearchViewModel.OrderDateEnd.HasValue)
            {
                Action<QueryDescriptor<ECommerce>> query = q => q.Range(r => r
                                                                            .DateRange(dr => dr
                                                                                .Field(f => f.OrderDate)
                                                                                .Lte(eCommerceSearchViewModel.OrderDateEnd.Value)
                                                                            )
                                                                        );

                queries.Add(query);
            }

            if (!string.IsNullOrEmpty(eCommerceSearchViewModel.Gender))
            {
                Action<QueryDescriptor<ECommerce>> query = q => q.Term(t => t
                                                                    .Field(f => f.CustomerGender)
                                                                    .Value(eCommerceSearchViewModel.Gender)
                                                                    .CaseInsensitive(true)
                                                                );

                queries.Add(query);
            }

            if (!queries.Any())
            {
                Action<QueryDescriptor<ECommerce>> query = q => q.MatchAll(m => { });
                queries.Add(query);
            }

            return await CalculateResultSetAsync(elasticsearchClient, page, pageSize, queries);
        }

        private static async Task<(List<ECommerce> list, long count)> CalculateResultSetAsync(ElasticsearchClient elasticsearchClient, int page, int pageSize, List<Action<QueryDescriptor<ECommerce>>> queries)
        {
            var pageFrom = (page - 1) * pageSize;

            var response = await elasticsearchClient.SearchAsync<ECommerce>(s => s
              .Index(indexName)
              .Size(pageSize)
              .From(pageFrom)
            .Query(q => q
                  .Bool(b => b
                      .Must(queries.ToArray())
                  )
              )
          );

            foreach (var hit in response.Hits) hit.Source.Id = hit.Id;

            return (list: response.Documents.ToList(), count: response.Total);
        }
    }
}