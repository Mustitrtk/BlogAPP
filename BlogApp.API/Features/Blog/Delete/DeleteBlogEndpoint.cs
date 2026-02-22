using AutoMapper;
using BlogApp.API.Repository;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Features.Blog.Delete
{
    public record DeleteBlogCommand(Guid Id) : IRequest<IResult>;

    public class DeleteBlogCommandHandler : IRequestHandler<DeleteBlogCommand, IResult>
    {
        private readonly AppDbContext _context;
        public DeleteBlogCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IResult> Handle(DeleteBlogCommand request, CancellationToken cancellationToken)
        {
            var blog = await _context.Blogs.FindAsync(request.Id);
            if (blog == null)
            {
                return Results.NotFound();
            }
            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync(cancellationToken);
            return Results.NoContent();
        }
    }

    public static class DeleteBlogEndpoint
    {
        public static RouteGroupBuilder DeleteBlogGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("/{Id:guid}", async ([FromServices] IMediator mediator, Guid Id) =>
            {
                var result = await mediator.Send(new DeleteBlogCommand(Id));
                return result;
            }).WithName("DeleteBlog");
            return group;
        }
    }
}
