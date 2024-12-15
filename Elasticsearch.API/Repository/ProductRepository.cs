using Elasticsearch.API.Dtos;
using Elasticsearch.API.Models;
using Nest;
using System.Collections.Immutable;

namespace Elasticsearch.API.Repository
{
    public class ProductRepository(ElasticClient client)
    {
        private const string indexName = "products";

        public async Task<Product?> SaveAsync(Product newProduct)
        {
            newProduct.CreatedDate = DateTime.Now;

            var response = await client.IndexAsync(newProduct, x => x.Index("products"));

            if (!response.IsValid) return null;

            newProduct.Id = response.Id;

            return newProduct;
        }

        public async Task<ImmutableList<Product>> GetAllAsync()
        {
            var response = await client.SearchAsync<Product>(s => s.Index(indexName).Query(q => q.MatchAll()));

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
            var response = await client.UpdateAsync<Product, ProductUpdateDto>(productUpdateDto.Id, x => x.Index(indexName).Doc(productUpdateDto));

            return response.IsValid;
        }

        public async Task<DeleteResponse> DeleteAsync(string id)
        {
            var response = await client.DeleteAsync<Product>(id, x => x.Index(indexName));

            return response;
        }
    }
}
