using ClientManagement.Core.Application.Dtos.Account;
using ClientManagement.Core.Application.Enums;
using ClientManagement.Core.Application.Interfaces.Services;
using ClientManagement.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace ClientManagement.Infrastructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountService(
              UserManager<ApplicationUser> userManager,
              SignInManager<ApplicationUser> signInManager
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #region Login & Logout
        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            AuthenticationResponse response = new();

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No Accounts registered with {request.Email}";
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Invalid credentials for {request.Email}";
                return response;
            }
            if (!user.EmailConfirmed)
            {
                response.HasError = true;
                response.Error = $"Account not confirmed for {request.Email}";
                return response;
            }

            response.Id = user.Id;
            response.Email = user.Email;
            response.UserName = user.UserName;

            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;

            return response;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
        #endregion

        #region Register
        public async Task<RegisterResponse> RegisterAdminAsync(RegisterRequest request)
        {
            RegisterResponse response = new()
            {
                HasError = false
            };

            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                response.HasError = true;
                response.Error = $"username '{request.UserName}' is already taken.";
                return response;
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"Email '{request.Email}' is already registered.";
                return response;
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                PhoneNumber = request.Phone,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.Admin.ToString());

            }
            else
            {
                response.HasError = true;
                response.Error = $"An error occurred while trying to register the admin.";
                return response;
            }

            return response;
        }
        #endregion

        #region Update
        public async Task<UpdateUserResponse> UpdateUserAsync(UpdateUserRequest request)
        {
            UpdateUserResponse response = new()
            {
                HasError = false
            };

            var userWithSameUserName = await _userManager.FindByNameAsync(request.Username);
            if (userWithSameUserName != null && userWithSameUserName.Id != request.Id)
            {
                response.HasError = true;
                response.Error = $"Username '{request.Username}' is already taken.";
                return response;
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail != null && userWithSameEmail.Id != request.Id)
            {
                response.HasError = true;
                response.Error = $"Email '{request.Email}'is already registered.";
                return response;
            }

            var user = await _userManager.FindByIdAsync(request.Id);

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.UserName = request.Username;
            user.PhoneNumber = request.Phone;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                if (request.Password != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    result = await _userManager.ResetPasswordAsync(user, token, request.Password);

                    if (!result.Succeeded)
                    {
                        response.HasError = true;
                        response.Error = $"An error ocurred while trying to update the password.";
                        return response;
                    }
                }
            }
            else
            {
                response.HasError = true;
                response.Error = $"An error ocurred while trying to update the user.";
                return response;
            }

            return response;
        }

        #endregion

    }
}
