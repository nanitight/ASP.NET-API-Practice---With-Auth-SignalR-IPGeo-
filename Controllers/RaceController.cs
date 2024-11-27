using Microsoft.AspNetCore.Mvc;
using RunGroupTUT;
using RunGroupTUT.Interfaces;
using RunGroupTUT.ViewModels;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    public class RaceController : Controller
    {
		private readonly IRaceRepository repository;
        private readonly IPhotoService photoService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public RaceController(IRaceRepository repository,IPhotoService photoService,IHttpContextAccessor httpContextAccessor)
        {
			this.repository = repository;
            this.photoService = photoService;
            this.httpContextAccessor = httpContextAccessor;
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
			var userId = httpContextAccessor.HttpContext?.User.GetUserId();
			var createRaceDTO = new CreateRaceViewModel
			{
				AppUserId = userId
			};
            return View(createRaceDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRaceViewModel raceDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await photoService.AddPhotoAsync(raceDTO.Image);
                Race newRace = new Race
                {
                    Title = raceDTO.Title,
                    Description = raceDTO.Description,
                    Image = result.Url.ToString(),
					AppUserId = raceDTO.AppUserId,
                    Address = new Address{
                        Street = raceDTO.Address.Street,
                        City = raceDTO.Address.City,
                        State = raceDTO.Address.State,
                    }
                };
                repository.Add(newRace);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo Upload error");
            }
            return View(raceDTO);
        }
		public async Task<IActionResult> Edit(int id)
		{
			var race = await repository.GetByIdAsync(id);
			if (race == null) return View("Error");
			var raceDTO = new EditRaceViewModel
			{
				Title = race.Title,
				Description = race.Description,
				RaceCategory = race.RaceCategory,
				AddressId = race.AddressId,
				Address = race.Address,
				URL = race.Image
			};
			return View(raceDTO);

		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, EditRaceViewModel raceDTO)
		{
			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("", "Failed to edit race");
				return View("Edit", raceDTO);
			}
			var userRace = await repository.GetByIdAsyncNoTracking(id);
			if (userRace != null)
			{
				try
				{
					var RES = await photoService.DeletePhotoAsync(userRace.Image);
				}
				catch (Exception ex)
				{
					ModelState.AddModelError("", "could not delete old photo.");
					return View(raceDTO);
				}
				var photoResult = await photoService.AddPhotoAsync(raceDTO.Image);

				var race = new Race
				{
					Id = id,
					Title = raceDTO.Title,
					Description = raceDTO.Description,
					Image = photoResult.Url.ToString(),
					AddressId = raceDTO.AddressId,
					Address = raceDTO.Address,
					RaceCategory = raceDTO.RaceCategory
				};

				repository.Update(race);
				return RedirectToAction("Index");
			}
			else { return View(raceDTO); }
		}

		public async Task<IActionResult> Delete(int id)
		{
			var race = await repository.GetByIdAsync(id);
			if (race == null) return View("Error");
			return View(race);
		}

		[HttpPost,ActionName("Delete")]
		public async Task<IActionResult> DeleteClub(int id)
		{
			var raceDetails = await repository.GetByIdAsync(id);
			if (raceDetails == null) return View("Error");
			repository.Delete(raceDetails);
			return RedirectToAction("Index");
		}
	}
}
