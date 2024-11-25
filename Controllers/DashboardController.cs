using Microsoft.AspNetCore.Mvc;
using RunGroupTUT.Interfaces;
using RunGroupTUT.ViewModels;
using WebApplication1.Data;

namespace RunGroupTUT.Controllers
{
	public class DashboardController : Controller
	{
        private readonly IDashboardRepository repository;

        public DashboardController(IDashboardRepository repository)
        {
            this.repository = repository;
        }
        public async Task<IActionResult> Index()
		{
            var userRaces = await repository.GetAllUserRaces();
            var userClubs = await repository.GetAllUserClubs();
            var dashboardDTO = new DashboardViewModel
            {
                Races = userRaces,
                Clubs = userClubs,
            };
			return View(dashboardDTO);
		}
	}
}
