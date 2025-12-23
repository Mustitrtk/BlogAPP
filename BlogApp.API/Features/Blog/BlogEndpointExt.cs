using BlogApp.API.Features.Blog.Create;
using BlogApp.API.Features.Blog.GetBlogs;
using BlogApp.API.Features.Blog.GetByCategoryId;
using BlogApp.API.Features.Blog.GetById;
using BlogApp.API.Features.Blog.Update;

namespace BlogApp.API.Features.Blog
{
    public static class BlogEndpointExt
    {
        public static void AddBlogGroupEndpointExt(this WebApplication app)
        {
            app.MapGroup("/blogs").WithTags("Blogs")
                .GetBlogsGroupItemEndpoint()
                .GetBlogByIdGroupItemEndpoint()
                .GetBlogByCategoryIdGroupItemEndpoint()
                .CreateBlogCommandGroupItemEndpoint()
                .UpdateBlogCommandGroupItemEndpoint();
        }
    }
}
