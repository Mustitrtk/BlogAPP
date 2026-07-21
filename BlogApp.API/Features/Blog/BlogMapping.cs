using Auth.API.Features.Auth;
using AutoMapper;
using BlogApp.API.Features.Blog.DTO;

namespace BlogApp.API.Features.Blog
{
    public class BlogMapping : Profile
    {
        public BlogMapping()
        {
            CreateMap<BlogEntity, BlogDTO>().ReverseMap();
            CreateMap<UserEntity, UserEntityDTO>().ReverseMap();
        }
    }
}
