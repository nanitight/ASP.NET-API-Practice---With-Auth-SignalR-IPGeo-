using WebApplication1.Models;

namespace RunGroupTUT.Interfaces
{
    public interface IDashboardRepository
    {
        public Task<List<Race>> GetAllUserRaces();
        public Task<List<Club>> GetAllUserClubs();
    }
}
