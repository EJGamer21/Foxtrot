using Foxtrot.Models;
using Foxtrot.Repositories.Contracts;

namespace Foxtrot.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(FoxtrotContext context) : base(context)
        {
        }
    }
}