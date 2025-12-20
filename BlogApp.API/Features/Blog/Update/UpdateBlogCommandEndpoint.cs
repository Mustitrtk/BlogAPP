using MediatR;

namespace BlogApp.API.Features.Blog.Update
{
    public static class UpdateBlogCommandEndpoint
    {
        public static RouteGroupBuilder UpdateBlogCommandGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPut("/", async (UpdateBlogCommand command, IMediator mediator) =>
            {
                return await mediator.Send(command);
            })
            .WithTags("Update");

            return group;
        }
    }
}
