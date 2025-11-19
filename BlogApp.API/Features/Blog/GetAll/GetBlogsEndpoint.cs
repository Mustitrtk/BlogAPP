using AutoMapper;
using BlogApp.API.Features.Blog.DTO;
using BlogApp.API.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.API.Features.Blog.GetBlogs
{
    public record GetBlogsQuery() : IRequest<List<BlogDTO>>;

    public class GetBlogsHandler : IRequestHandler<GetBlogsQuery, List<BlogDTO>>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public GetBlogsHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<BlogDTO>> Handle(GetBlogsQuery request, CancellationToken cancellationToken)
        {
            var blogs = await _context.Blogs
                .Include(b => b.Category)
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<BlogDTO>>(blogs);
        }
    }

    public static class GetBlogsEndpoint
    {
        public static RouteGroupBuilder GetBlogsGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new GetBlogsQuery());
                return Results.Ok(result);
            }).WithTags("GetBlogs");

            return group;
        }
    }
}
