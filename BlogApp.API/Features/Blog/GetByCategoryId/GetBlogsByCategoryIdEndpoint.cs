using AutoMapper;
using BlogApp.API.Features.Blog.DTO;
using BlogApp.API.Features.Blog.GetById;
using BlogApp.API.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.API.Features.Blog.GetByCategoryId
{
    public record GetBlogsByCategoryIdQuery(Guid CategoryId) : IRequest<List<BlogDTO>>;

    public class GetBlogsByCategoryIdHandler : IRequestHandler<GetBlogsByCategoryIdQuery, List<BlogDTO>>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public GetBlogsByCategoryIdHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<BlogDTO>> Handle(GetBlogsByCategoryIdQuery request, CancellationToken cancellationToken)
        {
            var blogs = await _context.Blogs
            .Include(x => x.Category)
            .Where(x => x.CategoryId == request.CategoryId)
            .ToListAsync(cancellationToken);

            if (blogs == null || blogs.Count == 0) return null;

            var blogsDto = _mapper.Map<List<BlogDTO>>(blogs);

            return blogsDto;
        }
    }

    public static class GetBlogsByCategoryIdEndpoint
    {
        public static RouteGroupBuilder GetBlogByCategoryIdGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{CategoryId:guid}", async (IMediator mediator, Guid CategoryId) =>
            {
                var result = await mediator.Send(new GetBlogByIdQuery(CategoryId));

                if (result == null) return Results.NotFound("Blogs not found!");

                return Results.Ok(result);
            }).WithTags("GetByCategoryId");

            return group;
        }
    }
}
