using BlogApp.API.Features.Blog.Create;
using BlogApp.API.Features.Category.GetAll;
using BlogApp.API.Features.Category.GetById;

namespace BlogApp.API.Features.Category
{
    public static class CategoryEndpointExt
    {
        public static void AddCategoryGroupEndpointExt(this WebApplication app)
        {
            app.MapGroup("/categories").WithTags("Categories")
                .GetCategoriesGroupItemEndpoint()
                .CreateBlogCommandGroupItemEndpoint()
                .GetByIdCategoryGroupEndpoint();
        }
    }
}
