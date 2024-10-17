using finance_api.DataTransfer.Users.Requests;
using finance_api.DataTransfer.Users.Responses;
using Microsoft.AspNetCore.Identity.Data;

namespace finance_api.Application.Users.Services.Interfaces
{
    public interface IUsersAppService
    {
        Task<string> LoginAsync(UserLoginRequest request);

        Task<UserResponse> RegisterAsync(UserRegisterRequest request);

        Task<UserResponse> ValidateAsync(int userId);
    }
}
