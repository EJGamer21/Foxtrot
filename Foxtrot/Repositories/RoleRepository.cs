using Foxtrot.Models;
using Foxtrot.Repositories.Contracts;

namespace Foxtrot.Repositories
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(FoxtrotContext context) : base(context)
        {
        }
    }
}