using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Repository;
using WebApplication1.Repository.Interfaces;

namespace WebApplication1.Controllers
{
    public class RaceController : Controller
    {
		private readonly IRaceRepository repository;

		public RaceController(IRaceRepository repository)
        {
			this.repository = repository;
		}
        public async Task<IActionResult> Index()
        {
            IEnumerable<Race> races = await repository.GetAll();
            return View(races);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Race race = await repository.GetByIdAsync(id);
            return View(race);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Race race)
        {
            if (!ModelState.IsValid)
            {
                return View(race);
            }
            repository.Add(race);
            return RedirectToAction("Index");
        }
    }
}
