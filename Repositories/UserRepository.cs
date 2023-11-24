using Backend.Data;
using Backend.Models;

namespace Backend.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TrangTraiContext _context;

        public UserRepository(TrangTraiContext context)
        {
            _context = context;
        }

        public ICollection<ApplicationUser> GetUsers()
        {
            return _context.Users.ToList();
        }

        public ApplicationUser GetUser(string id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return _context.Users.FirstOrDefault(u => u.Id == id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public ApplicationUser UpdateUser(ApplicationUser user)
        {
             _context.Update(user);
             _context.SaveChanges();

             return user;
        }
        public ApplicationUser DeleteUser(ApplicationUser user)
        {
            _context.Remove(user);
            _context.SaveChanges();
            return user;
        }

        public object GetUser(int? id)
        {
            throw new NotImplementedException();
        }

        public Task FirstOrDefaultAsync(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }
    }
}
