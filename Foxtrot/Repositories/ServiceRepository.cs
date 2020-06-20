using Foxtrot.Models;

namespace Foxtrot.Repositories
{
    public class ServiceRepository : BaseRepository<Service>
    {
        public ServiceRepository(FoxtrotContext context) : base(context)
        {
        }
    }
}