using BlogApp.API.Features.Category.DTO;
using MediatR;

namespace BlogApp.API.Features.Blog.Create
{
    public record CreateBlogCommand(string Title, string Description, string AuthorName, Guid CategoryId) : IRequest<IResult>;
}
