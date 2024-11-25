using RunGroupTUT.Interfaces;
using WebApplication1.Data;
using WebApplication1.Models;

namespace RunGroupTUT.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly AppDBContext context;
        private readonly IHttpContextAccessor httpContextAccessor;

        public DashboardRepository(AppDBContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<Club>> GetAllUserClubs()
        {
            var currentUser = httpContextAccessor.HttpContext?.User.GetUserId();
            var userClubs =  context.Clubs.Where(r => r.appUserId == currentUser);
            return userClubs.ToList();
        }

        public async Task<List<Race>> GetAllUserRaces()
        {
            var currentUser = httpContextAccessor.HttpContext?.User.GetUserId();
            var userRaces= context.Races.Where(r => r.AppUserId == currentUser);
            return userRaces.ToList();
        }
    }
}
