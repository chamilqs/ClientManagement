
using ClientManagement.Core.Application.Interfaces.Repositories;
using ClientManagement.Core.Domain.Entities;
using ClientManagement.Infrastructure.Persistence.Contexts;
using ClientManagement.Infrastructure.Persistence.Repositories;

namespace ClientManagement.Infrastucture.Persistence.Repositories
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        private readonly ApplicationContext _dbContext;

        public ClientRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }


    }
}