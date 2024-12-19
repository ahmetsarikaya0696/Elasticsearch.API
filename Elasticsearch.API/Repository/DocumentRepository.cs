using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Elasticsearch.API.Models;
using System.Collections.Immutable;

namespace Elasticsearch.API.Repository
{
    public class DocumentRepository(ElasticsearchClient client)
    {
        private const string indexName = "kibana_sample_data_ecommerce";

        public async Task<ImmutableList<Document>> TermLevelQueryAsync(string customerFirstName)
        {
            // 1.way
            //var response = await client.SearchAsync<Document>(s => s.Index(indexName).Query(q => q.Term(t => t.Field("customer_first_name.keyword").Value(customerFirstName))));

            // 2.way
            var response = await client.SearchAsync<Document>(s => s
                 .Index(indexName)
                 .Query(q => q
                     .Term(t => t
                         .Field(f => f.CustomerFirstName.Suffix("keyword"))
                         .Value(customerFirstName)
                     )
                 )
            );

            // 3. way
            //var termQuery = new TermQuery("customer_first_name.keyword") { Value = customerFirstName, CaseInsensitive = true };
            //var response = await client.SearchAsync<Document>(s => s.Index(indexName).Query(termQuery));

            foreach (var hit in response.Hits) hit.Source.Id = hit.Id;

            return response.Documents.ToImmutableList();
        }

        public async Task<ImmutableList<Document>> TermsLevelQueryAsync(List<string> customerFirstNameList)
        {
            IReadOnlyCollection<FieldValue> terms = [.. customerFirstNameList];

            // 1. way 
            //var termsQuery = new TermsQuery()
            //{
            //    Field = "customer_first_name.keyword",
            //    Terms = new TermsQueryField(terms)
            //};

            //var response = await client.SearchAsync<Document>(s => s.Query(termsQuery).Index(indexName));

            // 2.way
            var response = await client.SearchAsync<Document>(s => s
                .Index(indexName)
                .Query(q => q
                    .Terms(t => t
                        .Field(f => f.CustomerFirstName.Suffix("keyword"))
                        .Terms(new TermsQueryField(terms))
                    )
                )
            );

            foreach (var hit in response.Hits) hit.Source.Id = hit.Id;

            return response.Documents.ToImmutableList();
        }

        public async Task<ImmutableList<Document>> PrefixQueryAsync(string customerFullName)
        {
            var response = await client.SearchAsync<Document>(s => s
              .Index(indexName)
              .Query(q => q
                  .Prefix(p => p
                      .Field(f => f.CustomerFullName.Suffix("keyword"))
                      .Value(customerFullName)
                  )
              )
            );

            foreach (var hit in response.Hits) hit.Source.Id = hit.Id;

            return response.Documents.ToImmutableList();
        }

        public async Task<ImmutableList<Document>> RangeQueryAsync(double fromPrice, double toPrice)
        {
            var response = await client.SearchAsync<Document>(s => s
              .Index(indexName)
              .Query(q => q
                  .Range(r => r
                      .NumberRange(nr => nr
                        .Field(f => f.TaxfulTotalPrice)
                        .Gt(fromPrice)
                        .Lte(toPrice))
                  )
              )
            );

            foreach (var hit in response.Hits) hit.Source.Id = hit.Id;

            return response.Documents.ToImmutableList();
        }

        public async Task<ImmutableList<Document>> MatchAllQueryAsync()
        {
            var response = await client.SearchAsync<Document>(s => s
                 .Index(indexName)
                 .Query(q => q.MatchAll(_ => { }))
             );

            foreach (var hit in response.Hits) hit.Source.Id = hit.Id;

            return response.Documents.ToImmutableList();
        }

        public async Task<ImmutableList<Document>> MatchAllQueryPaginationAsync(int page, int size)
        {
            var pageFrom = (page - 1) * size;

            var response = await client.SearchAsync<Document>(s => s
                 .Index(indexName)
                 .From(pageFrom)
                 .Size(size)
                 .Query(q => q.MatchAll(_ => { }))
             );

            foreach (var hit in response.Hits) hit.Source.Id = hit.Id;

            return response.Documents.ToImmutableList();
        }

        public async Task<ImmutableList<Document>> WildcardQueryAsync(string customerFullName)
        {
            var response = await client.SearchAsync<Document>(s => s
              .Index(indexName)
              .Query(q => q
                  .Wildcard(w => w
                        .Field(f => f.CustomerFullName.Suffix("keyword"))
                        .Wildcard(customerFullName)
                  )
              )
            );

            foreach (var hit in response.Hits) hit.Source.Id = hit.Id;

            return response.Documents.ToImmutableList();
        }

        public async Task<ImmutableList<Document>> FuzzyQueryAsync(string customerFirstName)
        {
            var response = await client.SearchAsync<Document>(s => s
              .Index(indexName)
              .Query(q => q
                  .Fuzzy(fu => fu
                        .Field(f => f.CustomerFirstName.Suffix("keyword"))
                        .Value(customerFirstName)
                        .Fuzziness(new Fuzziness(1))
                  )
              ).Sort(s => s.Field(f => f.TaxfulTotalPrice, new FieldSort() { Order = SortOrder.Desc }))
            );

            foreach (var hit in response.Hits) hit.Source.Id = hit.Id;

            return response.Documents.ToImmutableList();
        }

        public async Task<ImmutableList<Document>> MatchQueryFullTextAsync(string categoryName)
        {
            var response = await client.SearchAsync<Document>(s => s
                .Index(indexName)
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.Category)
                        .Query(categoryName)
                    //.Operator(Operator.And)
                    )
                )
            );

            foreach (var hit in response.Hits) hit.Source.Id = hit.Id;

            return response.Documents.ToImmutableList();
        }

        public async Task<ImmutableList<Document>> MatchBooleanPrefixQueryFullTextAsync(string customerFullName)
        {
            var response = await client.SearchAsync<Document>(s => s
                .Index(indexName)
                .Query(q => q
                    .MatchBoolPrefix(m => m
                        .Field(f => f.CustomerFullName)
                        .Query(customerFullName)
                    )
                )
            );

            foreach (var hit in response.Hits) hit.Source.Id = hit.Id;

            return response.Documents.ToImmutableList();
        }

        public async Task<ImmutableList<Document>> MatchPhraseQueryFullTextAsync(string customerFullName)
        {
            var response = await client.SearchAsync<Document>(s => s
                .Index(indexName)
                .Query(q => q
                    .MatchPhrase(m => m
                        .Field(f => f.CustomerFullName)
                        .Query(customerFullName)
                    )
                )
            );

            foreach (var hit in response.Hits) hit.Source.Id = hit.Id;

            return response.Documents.ToImmutableList();
        }

        public async Task<ImmutableList<Document>> CompoundQueryExampleOneAsync(string cityName, double taxfulTotalPrice, string categoryName, string manufacturerName)
        {
            var response = await client.SearchAsync<Document>(s => s
                .Index(indexName)
                .Query(q => q
                    .Bool(b => b
                        .Must(m => m
                            .Term(t => t.Field(f => f.GeoIp.CityName)
                            .Value(cityName))
                        )
                        .MustNot(mn => mn
                            .Range(r => r
                                .NumberRange(nr => nr.Field(f => f.TaxfulTotalPrice)
                                .Lte(taxfulTotalPrice))
                            )
                        )
                        .Should(s => s
                            .Term(t => t.Field(f => f.Category.Suffix("keyword"))
                            .Value(categoryName))
                        )
                        .Filter(f => f
                            .Term(t => t.Field(f => f.Manufacturer.Suffix("keyword"))
                            .Value(manufacturerName))
                        )
                    )
                )
            );

            foreach (var hit in response.Hits) hit.Source.Id = hit.Id;

            return response.Documents.ToImmutableList();
        }

        public async Task<ImmutableList<Document>> CompoundQueryExampleTwoAsync(string customerFullName)
        {
            var response = await client.SearchAsync<Document>(s => s
                .Index(indexName)
                .Query(q => q
                    .Bool(b => b
                        .Should(s => s
                            .Match(m => m
                                .Field(f => f.CustomerFullName)
                                .Query(customerFullName)
                            ), s => s
                            .Prefix(p => p
                                .Field(f => f.CustomerFullName.Suffix("keyword"))
                                .Value(customerFullName)
                            )
                        )
                    )
                )
            );

            foreach (var hit in response.Hits) hit.Source.Id = hit.Id;

            return response.Documents.ToImmutableList();
        }

        public async Task<ImmutableList<Document>> MultiMatchQueryExampleAsync(string name)
        {
            var response = await client.SearchAsync<Document>(s => s
                .Index(indexName)
                .Query(q => q
                    .MultiMatch(mm => mm
                       .Fields(new Field("customer_first_name")
                       .And(new Field("customer_last_name"))
                       .And(new Field("customer_full_name"))
                       )
                       .Query(name)
                    )
                )
            );

            foreach (var hit in response.Hits) hit.Source.Id = hit.Id;

            return response.Documents.ToImmutableList();
        }
    }
}
