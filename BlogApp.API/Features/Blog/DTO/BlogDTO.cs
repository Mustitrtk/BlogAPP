using BlogApp.API.Features.Category.DTO;

namespace BlogApp.API.Features.Blog.DTO
{
    public record BlogDTO
    (
      Guid Id,
      string Title,
      string Description,
      string AuthorName,
      string? Picture,
      CategoryDTO Category
    );
}
