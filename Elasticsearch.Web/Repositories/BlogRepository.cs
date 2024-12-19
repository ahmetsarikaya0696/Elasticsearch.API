using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Elasticsearch.Web.Models;

namespace Elasticsearch.Web.Repositories
{
    public class BlogRepository(ElasticsearchClient elasticsearchClient)
    {
        private const string indexName = "blog";

        public async Task<Blog?> SaveAsync(Blog newBlog)
        {
            newBlog.CreatedDate = DateTime.Now;

            var response = await elasticsearchClient.IndexAsync(newBlog, x => x.Index(indexName));

            if (!response.IsValidResponse) return null;

            newBlog.Id = response.Id;

            return newBlog;
        }

        public async Task<List<Blog>> SearchAsync(string searchText)
        {
            // title => fulltext
            // content => fulltext
            List<Action<QueryDescriptor<Blog>>> queries = new();


            Action<QueryDescriptor<Blog>> matchAll = (q) => q.MatchAll(m => { });

            Action<QueryDescriptor<Blog>> matchContent = (q) => q.Match(m => m
                .Field(f => f.Content)
                .Query(searchText));

            Action<QueryDescriptor<Blog>> titleMatchBoolPrefix = (q) => q.MatchBoolPrefix(m => m
                .Field(f => f.Content)
                .Query(searchText));

            Action<QueryDescriptor<Blog>> tagTerm = (q) => q.Term(t => t.Field(f => f.Tags).Value(searchText));

            if (string.IsNullOrEmpty(searchText))
            {
                queries.Add(matchAll);
            }
            else
            {
                queries.Add(matchContent);
                queries.Add(matchContent);
                queries.Add(tagTerm);
            }

            var response = await elasticsearchClient.SearchAsync<Blog>(s => s
                .Index(indexName)
                .Query(q => q
                    .Bool(b => b
                        .Should(queries.ToArray())
                    )
                )
            );

            foreach (var hit in response.Hits) hit.Source.Id = hit.Id;

            return response.Documents.ToList();
        }
    }
}
