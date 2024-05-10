using ClientManagement.Core.Application.Interfaces.Services;
using ClientManagement.Core.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ClientManagement.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            #region Services
            services.AddTransient(typeof(IGenericService<,,>), typeof(GenericService<,,>));
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IClientService, ClientService>();
            services.AddTransient<IAddressService, AddressService>();

            #endregion
        }
    }
}
