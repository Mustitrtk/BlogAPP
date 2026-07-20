using Auth.API.Features.Auth.DTO;
using AutoMapper;

namespace Auth.API.Features.Auth
{
    public class AuthMapping : Profile
    {
        public AuthMapping()
        {
            CreateMap<UserEntity, UserRegisterDTO>().ReverseMap();
        }
    }
}
