using ClientManagement.Core.Application.Dtos.Account;
using ClientManagement.Core.Application.Helpers;
using ClientManagement.Core.Application.Interfaces.Services;
using ClientManagement.Core.Application.ViewModels.User;
using ClientManagement.Infrastructure.Identity.Entities;
using ClientManagement.Presentation.WebApp.Middlewares;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;

namespace ClientManagement.WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IClientService _clientService;
        private readonly IUserService _userService;
        private readonly AuthenticationResponse authViewModel;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(IUserService userService, IClientService clientService, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            authViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _clientService = clientService;
        }

        #region Login & Logout
        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> LoginPost(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            AuthenticationResponse userVm = await _userService.LoginAsync(vm);
            if (userVm != null && !userVm.HasError)
            {
                HttpContext.Session.Set("user", userVm);
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else
            {
                vm.HasError = userVm.HasError;
                vm.Error = userVm.Error;
                return View("Login", vm);
            }
        }

        // LogOut
        public async Task<IActionResult> LogOut()
        {
            await _userService.SignOutAsync();
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "User", action = "Login" });
        }
        #endregion

        #region Register
        public IActionResult SaveUser()
        {
            if (authViewModel == null || authViewModel.HasError)
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }
            return View(new SaveUserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> SaveUserPost(SaveUserViewModel vm)
        {
            if (authViewModel == null || authViewModel.HasError)
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Please fill all the required fields.";
                return View("SaveUser", vm);
            }

            if (vm.Password.IsNullOrEmpty())
            {
                ViewBag.Error = "Please fill all the required fields.";
                return View("SaveUser", vm);
            }

            string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]+$";
            if (!Regex.IsMatch(vm.Password, passwordPattern) || vm.Password.Length < 8)
            {
                ViewBag.Error = "Password must contain 8 characters and at least one number, one letter and one unique character such as !#$%&?.";
                return View("SaveUser", vm);
            }

            var response = await _userService.RegisterAsync(vm);
            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                ViewBag.Error = response.Error;
                return View(vm);
            }

            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region User
        public async Task<IActionResult> MySettings()
        {
            var user = await _userManager.FindByEmailAsync(authViewModel.Email);

            var vm = new SaveUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.PhoneNumber,
                Username = user.UserName
            };

            return View("SaveUser", vm);
        }

        public async Task<IActionResult> EditUserPost(SaveUserViewModel vm)
        {
            if (authViewModel == null || authViewModel.HasError)
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }

            if (!vm.Password.IsNullOrEmpty())
            {
                string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]+$";
                if (!Regex.IsMatch(vm.Password, passwordPattern) || vm.Password.Length < 8)
                {
                    ViewBag.Error = "Password must contain 8 characters and at least one number, one letter and one unique character such as !#$%&?.";
                    return View("SaveUser", vm);
                }

                if (vm.Password != vm.ConfirmPassword)
                {
                    ViewBag.Error = "Passwords must match.";
                    return View("SaveUser", vm);
                }

            }

            var response = await _userService.UpdateAsync(vm);
            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                ViewBag.Error = response.Error;
                return View(vm);
            }

            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Authorization
        public async Task<IActionResult> RedirectIndex(string? ReturnUrl)
        {
            return RedirectToRoute(new { controller = "User", action = "Index", hasError = true, message = "You don't have access to this section!" });
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
        #endregion
    }
}

