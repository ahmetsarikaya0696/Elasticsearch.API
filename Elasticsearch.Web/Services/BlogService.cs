using Elasticsearch.Web.Models;
using Elasticsearch.Web.Repositories;
using Elasticsearch.Web.ViewModels;

namespace Elasticsearch.Web.Services
{
    public class BlogService(BlogRepository blogRepository)
    {
        public async Task<bool> SaveAsync(BlogCreateViewModel blogCreateViewModel)
        {
            Blog newBlog = new()
            {
                Title = blogCreateViewModel.Title,
                Content = blogCreateViewModel.Content,
                Tags = blogCreateViewModel.Tags.Split(","),
                UserId = Guid.NewGuid(),
            };

            var blog = await blogRepository.SaveAsync(newBlog);

            return blog is not null;
        }

        public async Task<List<BlogViewModel>> SearchAsync(string searchText)
        {
            var blogList = await blogRepository.SearchAsync(searchText);
            var blogViewModelList = blogList.Select(x => new BlogViewModel()
            {
                Id = x.Id,
                Title = x.Title,
                Content = x.Content,
                Tags = x.Tags,
                CreatedDate = x.CreatedDate.ToString("dd.MM.yyyy HH:mm:ss"),
                UserId = x.UserId.ToString(),
            }).ToList();

            return blogViewModelList;
        }
    }
}
