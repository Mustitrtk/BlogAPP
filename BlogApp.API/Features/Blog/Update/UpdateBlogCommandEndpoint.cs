using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Features.Blog.Update
{
    public static class UpdateBlogCommandEndpoint
    {
        public static RouteGroupBuilder UpdateBlogCommandGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPut("/", async (UpdateBlogCommand command, [FromServices] IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return result;
            })
            .WithName("UpdateBlog").RequireAuthorization("AdminOnly");

            return group;
        }
    }
}
