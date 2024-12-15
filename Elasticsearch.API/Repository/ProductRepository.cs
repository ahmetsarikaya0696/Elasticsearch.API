using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Elasticsearch.API.Dtos;
using Elasticsearch.API.Models;
using System.Collections.Immutable;

namespace Elasticsearch.API.Repository
{
    public class ProductRepository(ElasticsearchClient client)
    {
        private const string indexName = "products";

        public async Task<Product?> SaveAsync(Product newProduct)
        {
            newProduct.CreatedDate = DateTime.Now;

            var response = await client.IndexAsync(newProduct, x => x.Index("products"));

            if (!response.IsValidResponse) return null;

            newProduct.Id = response.Id;

            return newProduct;
        }

        public async Task<ImmutableList<Product>> GetAllAsync()
        {
            var response = await client.SearchAsync<Product>(s => s.Index(indexName).Query(q => q.MatchAll(new MatchAllQuery())));

            foreach (var hit in response.Hits)
            {
                hit.Source.Id = hit.Id;
            }

            return response.Documents.ToImmutableList();
        }

        public async Task<Product?> GetByIdAsync(string id)
        {
            var response = await client.GetAsync<Product>(id, x => x.Index(indexName));

            if (!response.Found) return null;

            response.Source.Id = response.Id;

            return response.Source;
        }

        public async Task<bool> UpdateAsync(ProductUpdateDto productUpdateDto)
        {
            var response = await client.UpdateAsync<Product, ProductUpdateDto>(indexName, productUpdateDto.Id,x => x.Doc(productUpdateDto));

            return response.IsValidResponse;
        }

        public async Task<DeleteResponse> DeleteAsync(string id)
        {
            var response = await client.DeleteAsync<Product>(id, x => x.Index(indexName));

            return response;
        }
    }
}
