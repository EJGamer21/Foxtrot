using Foxtrot.Models;

namespace Foxtrot.Repositories
{
    public class RoleRepository : BaseRepository<Role>
    {
        public RoleRepository(FoxtrotContext context) : base(context)
        {
        }
    }
}