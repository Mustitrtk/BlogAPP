using BlogApp.API.Features.Blog.DTO;

namespace BlogApp.API.Features.Category.DTO
{
    public record CategoryWithBlogsDTO
    (
        Guid Id,
        string Name,
        List<BlogDTO> Blogs
    );
}