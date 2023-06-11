using AutoMapper;
using TrustGuard.Domain.Models;

namespace TrustGuard.Application.Dtos
{
    public class AutoMapperConfigs : Profile
    {
        public AutoMapperConfigs()
        {
            CreateMap<User, CreateUserDto>().ReverseMap();
        }
    }
}
