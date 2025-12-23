using AutoMapper;
using BlogApp.API.Features.Category.DTO;

namespace BlogApp.API.Features.Category
{
    public class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
        }
    }
}
