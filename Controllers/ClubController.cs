using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroupTUT.Interfaces;
using RunGroupTUT.ViewModels;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ClubController : Controller
    {
		private readonly IClubRepository clubRepository;
        private readonly IPhotoService photoService;

        public ClubController(IClubRepository repository, IPhotoService photoService)
        {
			this.clubRepository = repository;
            this.photoService = photoService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Club> clubs = await clubRepository.GetAll();
            return View(clubs);
        }

        //[HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            Club club = await clubRepository.GetByIdAsync(id);
            return View(club);
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Create(CreateClubViewModel clubDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await photoService.AddPhotoAsync(clubDTO.Image);
                var club = new Club
                {
                    Title = clubDTO.Title,
                    Description = clubDTO.Description,
                    Image = result.Url.ToString(),
                    Address = new Address{
                        Street = clubDTO.Address.Street,
                        City = clubDTO.Address.City,
                        State = clubDTO.Address.State,
                    },
                }; 
                clubRepository.Add(club);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(clubDTO);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var club = await clubRepository.GetByIdAsync(id);
            if (club == null) return View("Error");
            var clubDTO = new EditClubViewModel
            {
                Title = club.Title,
                Description = club.Description,
                ClubCategory = club.ClubCategory,
                AddressId = club.AddressId,
                Address = club.Address,
                URL = club.Image
            };
            return View(clubDTO);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,EditClubViewModel clubDTO)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit",clubDTO);
            }
            var userClub = await clubRepository.GetByIdAsyncNoTracking(id);
            if (userClub != null)
            {
                try
                {
                   var RES = await photoService.DeletePhotoAsync(userClub.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "could not delete old photo.");
                    return View(clubDTO);
                }
                var photoResult = await photoService.AddPhotoAsync(clubDTO.Image);

                var club = new Club
                {
                    Id = id,
                    Title = clubDTO.Title,
                    Description = clubDTO.Description,
                    Image = photoResult.Url.ToString(),
                    AddressId = clubDTO.AddressId,
                    Address = clubDTO.Address
                };

                clubRepository.Update(club);
                return RedirectToAction("Index");
            }
            else { return View(clubDTO); }
        }
    }


}
