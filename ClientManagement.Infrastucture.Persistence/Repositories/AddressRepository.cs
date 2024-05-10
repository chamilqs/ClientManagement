using ClientManagement.Core.Application.Interfaces.Repositories;
using ClientManagement.Core.Domain.Entities;
using ClientManagement.Infrastructure.Persistence.Contexts;
using ClientManagement.Infrastructure.Persistence.Repositories;

namespace ClientManagement.Infrastucture.Persistence.Repositories
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        private readonly ApplicationContext _dbContext;

        public AddressRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

    }
}