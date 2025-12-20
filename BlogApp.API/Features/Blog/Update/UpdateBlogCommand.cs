using MediatR;

namespace BlogApp.API.Features.Blog.Update
{
    public record UpdateBlogCommand(Guid BlogId, string Title, string Description, string AuthorName, string? Picture, Guid CategoryId) : IRequest<IResult>;
}
