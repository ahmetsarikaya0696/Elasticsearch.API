using Elastic.Clients.Elasticsearch;
using Elasticsearch.API.Dtos;
using Elasticsearch.API.Models;
using Elasticsearch.API.Repository;
using System.Collections.Immutable;
using System.Net;

namespace Elasticsearch.API.Services
{
    public class ProductService(ProductRepository productRepository, ILogger<ProductService> logger)
    {
        public async Task<ResponseDto<ProductDto>> SaveAsync(ProductCreateDto request)
        {
            var newProduct = new Product()
            {
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock,
                Feature = new ProductFeature()
                {
                    Color = ConvertStringToColorEnum(request),
                    Height = request.Feature.Height,
                    Width = request.Feature.Width,
                },
                CreatedDate = DateTime.Now,
            };

            var response = await productRepository.SaveAsync(newProduct);

            if (response is null) return ResponseDto<ProductDto>.Fail(["Kayıt esnasında hata meydana geldi"], HttpStatusCode.InternalServerError);

            var productDto = new ProductDto(
                response.Id,
                response.Name,
                response.Price,
                response.Stock,
                response.CreatedDate,
                response.UpdatedDate,
                response.Feature is null ? null : new ProductFeatureDto(response.Feature.Width, response.Feature.Height, response.Feature.Color.ToString())
            );


            return ResponseDto<ProductDto>.Success(productDto, HttpStatusCode.Created);
        }

        public async Task<ResponseDto<ImmutableList<ProductDto>>> GetAllAsync()
        {
            var products = await productRepository.GetAllAsync();

            var productsAsDto = products.Select(x => new ProductDto(
                x.Id,
                x.Name,
                x.Price,
                x.Stock,
                x.CreatedDate,
                x.UpdatedDate,
                x.Feature is null ? null : new ProductFeatureDto(x.Feature.Width, x.Feature.Height, x.Feature.Color.ToString()))).ToImmutableList();

            return ResponseDto<ImmutableList<ProductDto>>.Success(productsAsDto, HttpStatusCode.OK);
        }

        public async Task<ResponseDto<ProductDto>> GetByIdAsync(string id)
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product is null) return ResponseDto<ProductDto>.Fail(["Kayıtlı ürün bulunamadı"], HttpStatusCode.NotFound);

            var productAsDto = new ProductDto(
                product.Id,
                product.Name,
                product.Price,
                product.Stock,
                product.CreatedDate,
                product.UpdatedDate,
                product.Feature is null ? null : new ProductFeatureDto(product.Feature.Width, product.Feature.Height, product.Feature.Color.ToString()));

            return ResponseDto<ProductDto>.Success(productAsDto, HttpStatusCode.OK);
        }

        public async Task<ResponseDto<bool>> UpdateAsync(ProductUpdateDto productUpdateDto)
        {
            var isUpdated = await productRepository.UpdateAsync(productUpdateDto);

            if (!isUpdated) return ResponseDto<bool>.Fail(["Güncelleme sırasında bir hata oluştu"], HttpStatusCode.InternalServerError);

            return ResponseDto<bool>.Success(isUpdated, HttpStatusCode.NoContent);
        }

        public async Task<ResponseDto<bool>> DeleteAsync(string id)
        {
            var deleteResponse = await productRepository.DeleteAsync(id);

            if (!deleteResponse.IsValidResponse && deleteResponse.Result == Result.NotFound)
                return ResponseDto<bool>.Fail(["Silmeye çalıştığınız ürün bulunamamıştır"], HttpStatusCode.NotFound);

            if (!deleteResponse.IsValidResponse)
            {
                deleteResponse.TryGetOriginalException(out Exception? exception);
                logger.LogError(exception, deleteResponse.ElasticsearchServerError!.Error.ToString());
                
                return ResponseDto<bool>.Fail(["Silme işlemi sırasında bir hata oluştu"], HttpStatusCode.InternalServerError);
            }
            return ResponseDto<bool>.Success(deleteResponse.IsValidResponse, HttpStatusCode.NoContent);
        }

        private static ColorEnum ConvertStringToColorEnum(ProductCreateDto request)
        {
            var colorString = request.Feature.Color;
            ColorEnum colorEnum;
            if (Enum.TryParse<ColorEnum>(colorString, true, out var colorEnumValue))
            {
                colorEnum = colorEnumValue;
            }
            else
            {
                throw new ArgumentException($"Invalid value for ColorEnum: {colorString}");
            }

            return colorEnum;
        }
    }
}
