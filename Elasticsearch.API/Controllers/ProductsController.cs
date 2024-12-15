using Elasticsearch.API.Dtos;
using Elasticsearch.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Elasticsearch.API.Controllers
{
    public class ProductsController(ProductService productService) : CustomBaseController
    {
        [HttpPost]
        public async Task<IActionResult> Save(ProductCreateDto request)
        {
            return CreateActionResult(await productService.SaveAsync(request));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return CreateActionResult(await productService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            return CreateActionResult(await productService.GetByIdAsync(id));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(ProductUpdateDto request)
        {
            return CreateActionResult(await productService.UpdateAsync(request));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return CreateActionResult(await productService.DeleteAsync(id));
        }
    }
}
