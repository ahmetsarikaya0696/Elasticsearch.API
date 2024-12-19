using Elasticsearch.Web.Services;
using Elasticsearch.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Elasticsearch.Web.Controllers
{
    public class ECommerceController(ECommerceService eCommerceService) : Controller
    {
        public async Task<IActionResult> Search([FromQuery] SearchPageViewModel searchPageViewModel)
        {
            var (eCommerceList, totalCount, pageLinkCount) = await eCommerceService.SearchAsync(searchPageViewModel.ECommerceSearchViewModel, searchPageViewModel.Page, searchPageViewModel.PageSize);

            searchPageViewModel.ECommerceViewModelList = eCommerceList;
            searchPageViewModel.TotalCount = totalCount;
            searchPageViewModel.PageLinkCount = pageLinkCount;

            return View(searchPageViewModel);
        }
    }
}
