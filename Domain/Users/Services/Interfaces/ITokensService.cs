using finance_api.Domain.Users.Entities;

namespace finance_api.Domain.Users.Services.Interfaces
{
    public interface ITokensService
    {
        Task<string> GenerateToken(User user);
    }
}
