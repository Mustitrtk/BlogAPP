using AutoMapper;
using BlogApp.API.Features.Blog;
using BlogApp.API.Features.Blog.DTO;
using BlogApp.API.Features.Category.DTO;
using BlogApp.API.Repository;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BlogApp.API.Features.Category.GetById
{
    public record GetByIdCategoryQuery(Guid Id) : IRequest<CategoryWithBlogsDTO?>;

    public class GetByIdCategoryHandler : IRequestHandler<GetByIdCategoryQuery, CategoryWithBlogsDTO?>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public GetByIdCategoryHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CategoryWithBlogsDTO?> Handle(GetByIdCategoryQuery request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (category is null)
                return null;

            List <BlogEntity> blogs = await _context.Blogs
                .Where(b => b.CategoryId == request.Id)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var blogsDto = _mapper.Map<List<BlogListDTO>>(blogs);

            return new CategoryWithBlogsDTO(
                category.Id,
                category.Name,
                blogsDto
            );
        }
    }

    public static class GetByIdCategoryEndpoint
    {
        public static RouteGroupBuilder GetByIdCategoryGroupEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{Id:guid}", async ([FromServices] IMediator mediator, Guid Id) =>
            {
                var result = await mediator.Send(new GetByIdCategoryQuery(Id));
                if (result == null) return Results.NotFound("Category not found!");
                return Results.Ok(result);
            }).WithName("GetCategoryById").RequireAuthorization();
            return group;
        }
    }
}