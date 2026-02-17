using AutoMapper;
using BlogApp.API.Features.Blog.GetBlogs;
using BlogApp.API.Features.Category.DTO;
using BlogApp.API.Repository;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.API.Features.Category.GetAll
{
    public record GetCategoriesQuery : IRequest<List<CategoryDTO>>;

    public class GetCategoriesHandler : IRequestHandler<GetCategoriesQuery, List<CategoryDTO>>
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public GetCategoriesHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CategoryDTO>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _context.Categories.ToListAsync(cancellationToken);

            return _mapper.Map<List<CategoryDTO>>(categories);
        }
    }

    public static class GetCategoriesEndpoint
    {
        public static RouteGroupBuilder GetCategoriesGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/", async ([FromServices] IMediator mediator) =>
            {
                var result = await mediator.Send(new GetCategoriesQuery());
                return Results.Ok(result);
            }).WithTags("GetAll");

            return group;
        }
    }
}
