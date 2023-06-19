using AutoMapper;
using TrustGuard.Domain.Models;

namespace TrustGuard.Application.Dtos
{
    public class AutoMapperConfigs : Profile
    {
        public AutoMapperConfigs()
        {
            CreateMap<CascoInsurance, CreateCascoDto>().ReverseMap();
            CreateMap<User, CreateUserDto>().ReverseMap();
        }
    }
}
