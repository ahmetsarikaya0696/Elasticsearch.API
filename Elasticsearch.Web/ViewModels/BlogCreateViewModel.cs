namespace Elasticsearch.Web.ViewModels
{
    public class BlogCreateViewModel
    {
        public string Title { get; set; } = default!;

        public string Content { get; set; } = default!;

        public string Tags { get; set; } = default!;
    }
}
