using AutoMapper;
using BlogApp.API.Features.Category.DTO;
using BlogApp.API.Repository;
using MediatR;

namespace BlogApp.API.Features.Category.GetById
{
    public record GetByIdCategoryCommand(Guid id) : IRequest<CategoryDTO>;

    public class GetByIdCategoryHandler : IRequestHandler<GetByIdCategoryCommand, CategoryDTO>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public GetByIdCategoryHandler(AppDbContext context, IMapper mapper)
        {
             _context = context;
             _mapper = mapper;
        }
        public async Task<CategoryDTO> Handle(GetByIdCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FindAsync(request.id);

            if (category == null) return null;
            
            var categoryDto = _mapper.Map<CategoryDTO>(category);

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
                return result;
            }).WithTags("GetByIdCategory");
            return group;
        }
    }
}
