using Microsoft.AspNetCore.Mvc;
using RunGroupTUT.Interfaces;
using RunGroupTUT.ViewModels;
using System.Runtime.CompilerServices;

namespace RunGroupTUT.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository repository;

        public UserController(IUserRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("users")]
        public async Task<IActionResult> Index()
        {   
            var users = await repository.GetAllUsersAsync();
            List<UserViewModel> result = new List<UserViewModel>();
            foreach (var user in users)
            {
                var userVModel = new UserViewModel
                {
                    Id = user.Id,
                    Milage = user.Milage,
                    Pace = user.Pace,
                    UserName = user.UserName,
                };
                result.Add(userVModel);
            }
            return View(result);
        }

        public async Task<IActionResult> Detail(string id)
        {
            var user = await repository.GetUserByIdAsync(id);
            var userDTO = new UserDetailViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Pace = user.Pace,
                Milage = user.Milage,
            };
            return View(userDTO);
        }
    }
}
