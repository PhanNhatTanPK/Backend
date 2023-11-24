using Backend.Data;
using Microsoft.AspNetCore.Identity;

namespace Backend.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly TrangTraiContext _context;

        public RoleRepository(TrangTraiContext context)
        {
            _context = context;
        }

        public ICollection<IdentityRole> GetRoles()
        {
            return _context.Roles.ToList();
        }
    }
}
