using MediatR;

namespace BlogApp.API.Features.Blog.Create
{
    public static class CreateBlogCommandEndpoint
    {
        public static RouteGroupBuilder CreateBlogCommandGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreateBlogCommand command, IMediator mediator) =>
            {
                return await mediator.Send(command);
            })
            .WithTags("CreateBlog");

            return group;
        }
    }
}
