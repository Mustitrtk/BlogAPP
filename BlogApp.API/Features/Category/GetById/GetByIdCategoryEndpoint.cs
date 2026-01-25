using AutoMapper;
using BlogApp.API.Features.Category.DTO;
using BlogApp.API.Features.Blog.DTO;
using BlogApp.API.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.API.Features.Category.GetById
{
    public record GetByIdCategoryCommand(Guid id) : IRequest<CategoryWithBlogsDTO?>;

    public class GetByIdCategoryHandler : IRequestHandler<GetByIdCategoryCommand, CategoryWithBlogsDTO?>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public GetByIdCategoryHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CategoryWithBlogsDTO?> Handle(GetByIdCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories
                .Include(c => c.Blogs)
                .FirstOrDefaultAsync(c => c.Id == request.id, cancellationToken);

            if (category == null) return null;

            var blogsDto = category.Blogs is null
                ? new List<BlogDTO>()
                : _mapper.Map<List<BlogDTO>>(category.Blogs);

            var categoryDto = new CategoryWithBlogsDTO(category.Id, category.Name, blogsDto);

            return categoryDto;
        }
    }

    public static class GetByIdCategoryEndpoint
    {
        public static RouteGroupBuilder GetByIdCategoryGroupEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{Id:guid}", async (IMediator mediator, Guid Id) =>
            {
                var result = await mediator.Send(new GetByIdCategoryCommand(Id));
                if (result == null) return Results.NotFound("Category not found!");
                return Results.Ok(result);
            }).WithTags("GetByIdCategory");
            return group;
        }
    }
}