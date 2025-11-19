using BlogApp.API.Features.Blog.GetBlogs;

namespace BlogApp.API.Features.Blog
{
    public static class BlogEndpointExt
    {
        public static void AddBlogGroupEndpointExt(this WebApplication app)
        {
            app.MapGroup("/courses").WithTags("Blogs")
                .GetBlogsGroupItemEndpoint();
        }
    }
}
