using Foxtrot.Models;

namespace Foxtrot.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(FoxtrotContext context) : base(context)
        {
        }
    }
}