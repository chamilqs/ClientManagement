using ClientManagement.Core.Application.Dtos.Account;

namespace ClientManagement.Core.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<RegisterResponse> RegisterAdminAsync(RegisterRequest request);
        Task<UpdateUserResponse> UpdateUserAsync(UpdateUserRequest request);
        Task SignOutAsync();
    }
}