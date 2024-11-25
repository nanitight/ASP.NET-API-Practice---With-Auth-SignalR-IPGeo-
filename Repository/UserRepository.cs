using Microsoft.EntityFrameworkCore;
using RunGroupTUT.Interfaces;
using WebApplication1.Data;
using WebApplication1.Models;

namespace RunGroupTUT.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDBContext context;

        public UserRepository(AppDBContext context)
        {
            this.context = context;
        }
        public bool Add(AppUser user)
        {
            throw new NotImplementedException();
        }

        public bool Delete(AppUser user)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AppUser>> GetAllUsersAsync()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<AppUser> GetUserByIdAsync(string id)
        {
            return await context.Users.FindAsync(id);
        }

        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(AppUser user)
        {
            context.Update(user);
            return Save();
        }
    }
}
