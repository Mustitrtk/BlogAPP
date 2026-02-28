namespace BlogApp.API.Features.Blog.DTO
{
    public record BlogListDTO(
       Guid Id,
       string Title,
       string Description,
       string AuthorName,
       string? Picture
    );
}
