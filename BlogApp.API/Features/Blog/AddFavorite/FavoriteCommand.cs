using MediatR;

namespace BlogApp.API.Features.Blog.Favorite
{
    public record FavoriteCommand(Guid userId, Guid blogId) : IRequest<IResult>;
}
