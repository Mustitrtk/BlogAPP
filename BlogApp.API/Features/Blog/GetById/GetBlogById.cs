using AutoMapper;
using BlogApp.API.Features.Blog.DTO;
using BlogApp.API.Features.Blog.GetBlogs;
using BlogApp.API.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.API.Features.Blog.GetById
{
    public record GetBlogByIdQuery(Guid Id) : IRequest<BlogDTO>;

    public class GetBlogByIdHandler : IRequestHandler<GetBlogByIdQuery, BlogDTO>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public GetBlogByIdHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BlogDTO> Handle(GetBlogByIdQuery request, CancellationToken cancellationToken)
        {
            var blog = await _context.Blogs
            .Include(x => x.Category)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (blog is null) return null;

            var blogDto = _mapper.Map<BlogDTO>(blog);

            return blogDto;
        }
    }

    public static class GetBlogById
    {
        public static RouteGroupBuilder GetBlogByIdGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{Id:guid}", async (IMediator mediator, Guid Id) =>
            {
                var result = await mediator.Send(new GetBlogByIdQuery(Id));

                if (result == null) return Results.NotFound("Blog not found!");

                return Results.Ok(result);
            }).WithTags("GetBlog");

            return group;
        }
    }
}
