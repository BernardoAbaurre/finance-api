using AutoMapper;
using Azure.Core;
using finance_api.Application.Users.Services.Interfaces;
using finance_api.DataTransfer.Users.Requests;
using finance_api.DataTransfer.Users.Responses;
using finance_api.Domain.Users.Entities;
using finance_api.Domain.Users.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;

namespace finance_api.Application.Users.Services
{
    public class UsersAppService : IUsersAppService
    {
        private readonly IUsersService usersService;
        private readonly IMapper mapper;

        public UsersAppService(IMapper mapper, IUsersService usersService)
        {
            this.mapper = mapper;
            this.usersService = usersService;
        }

        public async Task<string> LoginAsync(UserLoginRequest request)
        {
            return await usersService.LoginAsync(request.Email, request.Password);
        }

        public async Task<UserResponse> RegisterAsync(UserRegisterRequest request)
        {
            User user = await usersService.InsertAsync(request.FirstName, request.LastName, request.Email, request.Password);
            
            return mapper.Map<UserResponse>(user);
        }

        public async Task<UserResponse> ValidateAsync(int id)
        {
            User user = await usersService.ValidateAsync(id);

            return mapper.Map<UserResponse>(user);
        }
    }
}
