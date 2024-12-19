namespace Elasticsearch.Web.ViewModels
{
    public class BlogViewModel
    {
        public string Id { get; set; } = default!;

        public string Title { get; set; } = default!;

        public string Content { get; set; } = default!;

        public string[] Tags { get; set; } = [];

        public string UserId { get; set; } = default!;

        public string CreatedDate { get; set; } = default!;
    }
}
