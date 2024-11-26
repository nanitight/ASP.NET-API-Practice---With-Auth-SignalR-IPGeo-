using WebApplication1.Models;

namespace RunGroupTUT.Interfaces
{
    public interface IDashboardRepository
    {
        public Task<List<Race>> GetAllUserRaces();
        public Task<List<Club>> GetAllUserClubs();

        public Task<AppUser> GetUserByIdAsync(string id);

        public Task<AppUser> GetUserByIdNoTrackingAsync(string id);

        public Task<AppUser> GetUserByEmailAsync(string email);
        public bool Update(AppUser user);
        public bool Save();
    }
}
