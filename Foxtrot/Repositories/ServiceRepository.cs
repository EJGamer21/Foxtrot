using Foxtrot.Models;
using Foxtrot.Repositories.Contracts;

namespace Foxtrot.Repositories
{
    public class ServiceRepository : BaseRepository<Service>, IServiceRepository
    {
        public ServiceRepository(FoxtrotContext context) : base(context)
        {
        }
    }
}