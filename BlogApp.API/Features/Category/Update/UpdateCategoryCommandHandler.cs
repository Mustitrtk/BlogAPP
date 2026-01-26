using AutoMapper;
using BlogApp.API.Features.Category.DTO;
using BlogApp.API.Repository;
using MediatR;

namespace BlogApp.API.Features.Category.Update
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, IResult>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public UpdateCategoryCommandHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IResult> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FindAsync(request.Id);

            if (category == null) return Results.NotFound("Category not found!");

            category.Name = request.Name;

            _context.Categories.Update(category);

            await _context.SaveChangesAsync(cancellationToken);

            return Results.Ok(category);
        }
    }
}
