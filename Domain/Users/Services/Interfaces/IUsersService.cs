using finance_api.Domain.Users.Entities;

namespace finance_api.Domain.Users.Services.Interfaces
{
    public interface IUsersService
    {
        Task<User> ValidateAsync(int id);
        Task<User> InsertAsync(string firstName, string lastName, string email, string password);
        Task<string> LoginAsync(string email, string password);
    }
}
