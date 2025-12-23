using MediatR;

namespace BlogApp.API.Features.Category.Create
{
    public record CreateCategoryCommand(string Name) : IRequest<IResult>;
}
