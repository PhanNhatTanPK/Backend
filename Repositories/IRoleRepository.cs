using Microsoft.AspNetCore.Identity;

namespace Backend.Repositories
{
    public interface IRoleRepository
    {
        ICollection<IdentityRole> GetRoles();
    }
}
