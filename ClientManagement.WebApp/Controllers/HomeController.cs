using ClientManagement.Core.Application.Dtos.Account;
using ClientManagement.Core.Application.Helpers;
using ClientManagement.Core.Application.Interfaces.Services;
using ClientManagement.Core.Application.ViewModels.Client;
using ClientManagement.Core.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClientManagement.WebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IClientService _clientService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse authViewModel;

        public HomeController(IClientService clientService, IHttpContextAccessor httpContextAccessor)
        {
            _clientService = clientService;
            _httpContextAccessor = httpContextAccessor;
            authViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            var clients = await _clientService.GetAllViewModelWithInclude();
            if (clients == null || clients.Count <= 0)
            {
                return View();
            }

            return View(clients);
        }
        #endregion

        #region Add & Update Client
        public async Task<IActionResult> NewClient()
        {
            SaveClientViewModel model = new SaveClientViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddClient(SaveClientViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("NewClient", viewModel);

            }

            await _clientService.Add(viewModel);
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }

        [HttpPost]
        public async Task<IActionResult> AddAddress(int ClientId, string Country, string Street, string City, string State, string PostalCode)
        {
            var clients = await _clientService.GetAllViewModelWithInclude();
            if (string.IsNullOrEmpty(Country) || string.IsNullOrEmpty(Street) || string.IsNullOrEmpty(City) || string.IsNullOrEmpty(PostalCode))
            {
                ViewBag.Error = "To add a new address, please complete all the required fields (Country, Street, City, Postal Code).";
                return View("Index", clients);
            }

            Address address = new Address
            {
                Country = Country,
                Street = Street,
                City = City,
                State = State,
                PostalCode = PostalCode
            };

            var newAddress = await _clientService.AddAddress(ClientId, address);

            if (newAddress != null)
            {
                ViewBag.Success = "Address added successfully!";
            }

            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }
        #endregion

        #region Delete Client
        public async Task<IActionResult> DeleteClient(int id)
        {
            SaveClientViewModel clientvm = await _clientService.GetByIdSaveViewModel(id);
            return View(clientvm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteClientPost(int id)
        {
            try
            {
                await _clientService.Delete(id);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            return RedirectToAction("Index");
        }
        #endregion
    }
}
