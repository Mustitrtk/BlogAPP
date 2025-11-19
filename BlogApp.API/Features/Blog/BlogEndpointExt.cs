using BlogApp.API.Features.Blog.GetBlogs;
using BlogApp.API.Features.Blog.GetById;

namespace BlogApp.API.Features.Blog
{
    public static class BlogEndpointExt
    {
        public static void AddBlogGroupEndpointExt(this WebApplication app)
        {
            app.MapGroup("/courses").WithTags("Blogs")
                .GetBlogsGroupItemEndpoint()
                .GetBlogByIdGroupItemEndpoint();
        }
    }
}
