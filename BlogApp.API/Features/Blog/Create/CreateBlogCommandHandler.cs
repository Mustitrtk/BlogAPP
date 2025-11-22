using AutoMapper;
using BlogApp.API.Repository;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.API.Features.Blog.Create
{
    public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommand,IResult>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CreateBlogCommandHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IResult> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
        {
            var hasCategory = await _context.Categories.FindAsync(request.CategoryId, cancellationToken);

            if (hasCategory is null) return Results.NotFound("Category not found !");

            var hasBlog = await _context.Blogs.AnyAsync(x=> x.Title == request.Title);

            if (hasBlog) return Results.BadRequest("Blog title must be unique!");

            var newBlog = _mapper.Map<BlogEntity>(request);

            newBlog.Id = NewId.NextSequentialGuid();

            _context.Blogs.Add(newBlog);

            await _context.SaveChangesAsync(cancellationToken);

            return Results.Created($"/blogs/{newBlog.Id}", newBlog.Id);
        }
    }
}
