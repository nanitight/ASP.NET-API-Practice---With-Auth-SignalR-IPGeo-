using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using RunGroupTUT.Interfaces;
using RunGroupTUT.ViewModels;
using System.Security.Claims;
using WebApplication1.Data;
using WebApplication1.Models;

namespace RunGroupTUT.Controllers
{
	public class DashboardController : Controller
	{
        private readonly IDashboardRepository repository;
		private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IPhotoService photoService;
        
		public DashboardController(IDashboardRepository repository, IHttpContextAccessor httpContextAccessor ,
            IPhotoService photoService)
        {
            this.repository = repository;
			this.httpContextAccessor = httpContextAccessor;
            this.photoService = photoService;
		}

        private void MapUserEdit(AppUser user,EditUserDashboardViewModel editDTO, ImageUploadResult photoResults)
        {
            user.Id = editDTO.Id;
            user.Pace = editDTO.Pace;
            user.Milage = editDTO.Milage; 
            user.ProfileImageUrl = photoResults.Url.ToString();
            user.City = editDTO.City;
            user.State = editDTO.State;

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

        public async Task<IActionResult> EditUserProfile()
        {
            var currUserEmail = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            var user = await repository.GetUserByEmailAsync(currUserEmail);
            if (user == null)
            {
                return View("Error");
            }
            var editUserDTO = new EditUserDashboardViewModel
            {
                Id = user.Id,
                Pace = user.Pace,
                Milage = user.Milage,
                ProfileImageUrl = user.ProfileImageUrl,
                City = user.City,
                State = user.State
            }; 
            return View(editUserDTO);

        }

        [HttpPost]
        public async Task<IActionResult> EditUserProfile(EditUserDashboardViewModel editDTO)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit profile");
                return View("EditUserProfile", editDTO);
            }

            var user = await repository.GetUserByIdNoTrackingAsync(editDTO.Id);
            var photoResult = await photoService.AddPhotoAsync(editDTO.Image);
            if (user.ProfileImageUrl == "" || user.ProfileImageUrl == null)
            {
                MapUserEdit(user, editDTO, photoResult);
                repository.Update(user);
                return RedirectToAction("Index");
            }
            else
            {
                try
                {
                    await photoService.DeletePhotoAsync(user.ProfileImageUrl);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(editDTO);
                }
                
                MapUserEdit(user, editDTO, photoResult);
                repository.Update(user);
                return RedirectToAction("Index");
            }
        }
	}
}
