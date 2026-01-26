using MediatR;

namespace BlogApp.API.Features.Category.Update
{
    public record UpdateCategoryCommand(Guid Id, string Name) : IRequest<IResult>;
}
