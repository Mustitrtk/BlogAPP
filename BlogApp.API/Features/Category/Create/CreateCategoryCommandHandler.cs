using AutoMapper;
using BlogApp.API.Repository;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.API.Features.Category.Create
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, IResult>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IResult> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var hasCategory = await _context.Categories.AnyAsync(x=> x.Name == request.Name);

            if (hasCategory) return Results.BadRequest("Category already added.");

            var newCategory = _mapper.Map<Category>(request);

            newCategory.Id = NewId.NextSequentialGuid();

            _context.Categories.Add(newCategory);

            await _context.SaveChangesAsync();

            return Results.Created($"/categories/{newCategory.Id}", newCategory.Id);
        }
    }
}
