using AutoMapper;
using HealthService.Dtos;
using HealthService.Models;

namespace HealthService.Porfiles
{
    public class UsersProfile : Profile
    {
            public UsersProfile()
{
   //Source -> Target 
   CreateMap<User,UserReadDto>();
   CreateMap <UserCreateDto,User>();
}
    }
}