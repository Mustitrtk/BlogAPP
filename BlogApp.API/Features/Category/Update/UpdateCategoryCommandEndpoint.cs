using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Features.Category.Update
{
    public static class UpdateCategoryCommandEndpoint
    {
        public static RouteGroupBuilder UpdateCategoryGroupEndpoint(this RouteGroupBuilder group)
        {
            group.MapPut("/", async (UpdateCategoryCommand command, [FromServices] IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return result;
            }).WithTags("UpdateCategory");

            return group;
        }
    }
}
