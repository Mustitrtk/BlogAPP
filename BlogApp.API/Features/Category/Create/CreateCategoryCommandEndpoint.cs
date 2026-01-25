using MediatR;

namespace BlogApp.API.Features.Category.Create
{
    public static class CreateCategoryCommandEndpoint
    {
        public static RouteGroupBuilder CreateCategoryCommandGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreateCategoryCommand command, IMediator mediator) =>
            {
                return await mediator.Send(command);
            })
            .WithTags("Create");
            return group;
        }
    }
}
