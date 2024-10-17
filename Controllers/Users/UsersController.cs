using finance_api.Application.Users.Services.Interfaces;
using finance_api.DataTransfer.Users.Requests;
using finance_api.DataTransfer.Users.Responses;
using finance_api.Domain.Users.Entities;
using finance_api.Domain.Users.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace finance_api.Controllers.Users
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly ITokensService tokensService;
        private readonly IUsersAppService usersAppService;

        public UsersController(UserManager<User> userManager, ITokensService tokensService, IUsersAppService usersAppService)
        {
            this.userManager = userManager;
            this.tokensService = tokensService;
            this.usersAppService = usersAppService;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponse>> ValidateAsync(int id)
        {
            var response = await usersAppService.ValidateAsync(id);

            return Ok(response);
        }

        [HttpPost("logins")]
        public async Task<ActionResult<string>> LoginAsync([FromBody] UserLoginRequest request)
        {
            var token = await usersAppService.LoginAsync(request);

            return Ok(token);
        }

        [HttpPost("registers")]
        public async Task<ActionResult<UserResponse>> RegisterAsync([FromBody] UserRegisterRequest request)
        {
            var response = await usersAppService.RegisterAsync(request);

            return Ok(response);
        }
    }
}
