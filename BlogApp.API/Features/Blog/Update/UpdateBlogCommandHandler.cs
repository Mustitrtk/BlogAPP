using AutoMapper;
using BlogApp.API.Repository;
using MediatR;

namespace BlogApp.API.Features.Blog.Update
{
    public class UpdateBlogCommandHandler : IRequestHandler<UpdateBlogCommand, IResult>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public UpdateBlogCommandHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IResult> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
        {
            var blog = await _context.Blogs.FindAsync(request.BlogId, cancellationToken);

            if (blog == null) return Results.NotFound("Blog is not found");

            blog.Title = request.Title;
            blog.Description = request.Description;
            blog.AuthorName = request.AuthorName;
            // TODO : Picture should added.
            blog.CategoryId = request.CategoryId;

            _context.Blogs.Update(blog);

            await _context.SaveChangesAsync(cancellationToken);

            return Results.Ok(blog);
        }
    }
}
