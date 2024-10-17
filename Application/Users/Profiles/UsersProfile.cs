using AutoMapper;
using finance_api.DataTransfer.Users.Responses;
using finance_api.Domain.Users.Entities;

namespace finance_api.Application.Users.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<User, UserResponse>();
        }
    }
}
