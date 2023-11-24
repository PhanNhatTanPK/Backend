
using Backend.Models;

namespace Backend.Repositories
{
    public interface IUserRepository
    {
        ICollection<ApplicationUser> GetUsers();

        ApplicationUser GetUser(string id);

        ApplicationUser UpdateUser(ApplicationUser user);
        object GetUser(int? id);
        Task FirstOrDefaultAsync(Func<object, bool> value);
    }
}
