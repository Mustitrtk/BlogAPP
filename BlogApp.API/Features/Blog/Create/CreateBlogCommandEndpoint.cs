using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Features.Blog.Create
{
    public static class CreateBlogCommandEndpoint
    {
        public static RouteGroupBuilder CreateBlogCommandGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreateBlogCommand command, [FromServices] IMediator mediator) =>
            {
                return await mediator.Send(command);
            })
            .WithTags("Create");

            return group;
        }
    }
}
