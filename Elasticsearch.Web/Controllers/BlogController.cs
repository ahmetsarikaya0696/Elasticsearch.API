using Elasticsearch.Web.Services;
using Elasticsearch.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Elasticsearch.Web.Controllers
{
    public class BlogController(BlogService blogService) : Controller
    {
        [HttpGet]
        public IActionResult Save()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(BlogCreateViewModel blogCreateViewModel)
        {
            bool isSuccess = await blogService.SaveAsync(blogCreateViewModel);

            if (!isSuccess)
            {
                TempData["result"] = "Kayıt Başarısız!";
                return RedirectToAction(nameof(BlogController.Save));
            }

            TempData["result"] = "Kayıt Başarılı!";
            return RedirectToAction(nameof(BlogController.Save));
        }

        [HttpGet]
        public async Task<IActionResult> Search()
        {
            return View(await blogService.SearchAsync(string.Empty));
        }

        [HttpPost]
        public async Task<IActionResult> Search(string searchText)
        {
            TempData["searchText"] = searchText;
            return View(await blogService.SearchAsync(searchText));
        }
    }
}
