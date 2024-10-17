using Azure.Core;
using finance_api.Domain.Users.Entities;
using finance_api.Domain.Users.Exceptions;
using finance_api.Domain.Users.Services.Interfaces;
using finance_api.Domain.Utils.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace finance_api.Domain.Users.Services
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<User> userManager;
        private readonly ITokensService tokensService;

        public UsersService(UserManager<User> userManager, ITokensService tokensService)
        {
            this.userManager = userManager;
            this.tokensService = tokensService;
        }

        public async Task<User> InsertAsync(string firstName, string lastName, string email, string password)
        {
            var existingUser = await userManager.FindByEmailAsync(email);

            if (existingUser != null)
            {
                throw new UserAlreadyExistsException(email);
            }

            var user = new User(firstName, lastName, email);

            var result = await userManager.CreateAsync(user, password);
            
            if(result.Succeeded)
            {
                return user;
            }
            else
            {
                throw new CouldNotCreateUserException(result.Errors);
            }
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user != null && await userManager.CheckPasswordAsync(user, password))
            {
                var token = await tokensService.GenerateToken(user);
                return token;
            }
            
            throw new InvalidLoginException(email, password);
            
        }

        public async Task<User> ValidateAsync(int id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                throw new RegisterNotFound(id);
            }

            return user;
        }
    }
}
