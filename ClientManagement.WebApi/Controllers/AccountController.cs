using ClientManagement.Core.Application.Dtos.Account;
using ClientManagement.Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClientManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Authenticates a user asynchronously.
        /// </summary>
        /// <param name="request">The authentication request.</param>
        /// <returns>The result of the authentication.</returns>
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request)
        {
            return Ok(await _accountService.AuthenticateAsync(request));
        }

        /// <summary>
        /// Registers an admin asynchronously.
        /// </summary>
        /// <param name="request">The registration request.</param>
        /// <returns>The result of the registration.</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisterAdminAsync(RegisterRequest request)
        {
            return Ok(await _accountService.RegisterAdminAsync(request));
        }

    }
}
