using ClientManagement.Core.Application.Dtos.Account;
using ClientManagement.Core.Application.ViewModels.User;

namespace ClientManagement.Core.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<AuthenticationResponse> LoginAsync(LoginViewModel vm);
        Task<RegisterResponse> RegisterAsync(SaveUserViewModel vm);
        Task<UpdateUserResponse> UpdateAsync(SaveUserViewModel vm);
        Task SignOutAsync();
    }
}