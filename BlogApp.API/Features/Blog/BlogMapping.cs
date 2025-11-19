using AutoMapper;
using BlogApp.API.Features.Blog.DTO;

namespace BlogApp.API.Features.Blog
{
    public class BlogMapping : Profile
    {
        public BlogMapping()
        {
            CreateMap<BlogEntity, BlogDTO>().ReverseMap();
        }
    }
}
