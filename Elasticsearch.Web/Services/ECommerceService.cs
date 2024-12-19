using Elasticsearch.Web.Repositories;
using Elasticsearch.Web.ViewModels;

namespace Elasticsearch.Web.Services
{
    public class ECommerceService(EcommerceRepository ecommerceRepository)
    {
        public async Task<(List<ECommerceViewModel> list, long totalCount, long pageLinkCount)> SearchAsync(ECommerceSearchViewModel eCommerceSearchViewModel, int page, int pageSize)
        {
            var (eCommerceList, totalCount) = await ecommerceRepository.SearchAsync(eCommerceSearchViewModel, page, pageSize);

            var pageLinkCount = (totalCount / pageSize);
            if (totalCount % pageSize != 0) pageLinkCount += 1;

            var eCommerceListViewModel = eCommerceList.Select(x => new ECommerceViewModel()
            {
                Id = x.Id,
                OrderId = x.OrderId,
                Category = string.Join(",", x.Category),
                CustomerFullName = x.CustomerFullName,
                CustomerFirstName = x.CustomerFirstName,
                CustomerLastName = x.CustomerLastName,
                OrderDate = x.OrderDate!.Value.ToShortDateString(),
                CustomerGender = x.CustomerGender.ToLower(),
                TaxfulTotalPrice = x.TaxfulTotalPrice,
            }).ToList();

            return(eCommerceListViewModel, totalCount, pageLinkCount);
        }
    }
}
