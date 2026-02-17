using AutoMapper;
using BlogApp.API.Repository;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Features.Category.Delete
{

    public record DeleteCategoryCommand(Guid Id) : IRequest<IResult>;

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, IResult>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public DeleteCategoryCommandHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IResult> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FindAsync(request.Id);

            if (category == null)
            {
                return Results.NotFound("Category not found !");
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync(cancellationToken);
            return Results.Ok("Category deleted successfully !");
        }
    }

    public static class DeleteCategoryEndpoints
    {
        public static RouteGroupBuilder DeleteCategoryEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("/{Id:guid}", async ([FromServices] IMediator mediator, Guid Id) =>
            {
                var result = await mediator.Send(new DeleteCategoryCommand(Id));
                return result;
            }).WithTags("DeleteCategory");
            return group;
        }
    }
}
