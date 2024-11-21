using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RunGroupTUT.ViewModels;
using WebApplication1.Data;
using WebApplication1.Models;

namespace RunGroupTUT.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<AppUser> userManager;
		private readonly SignInManager<AppUser> signInManager;
		private readonly AppDBContext context;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager, AppDBContext context)
        {
            this.userManager = userManager;
			this.signInManager = signInManager;
			this.context = context;
        }
        public IActionResult Login()
		{
			var response = new LoginViewModel();
			return View(response);
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel loginDTO)
		{
			if (!ModelState.IsValid)
			{
				return View(loginDTO);
			}
			var user = await userManager.FindByEmailAsync(loginDTO.EmailAddress);
			if (user != null)
			{
				var passwordCheck = await userManager.CheckPasswordAsync(user,loginDTO.Password);
				if (passwordCheck)
				{
					var result = await signInManager.PasswordSignInAsync(user, loginDTO.Password,false,false);
					if (result.Succeeded)
					{
						return RedirectToAction("Index", "Club");
					}
				}
				//pass incorrect
				TempData["Error"] = "Wrong credentials. Please, try again";
				return View(loginDTO);
			}
			TempData["Error"] = "Wrong credentials. Please, try again";
			return View(loginDTO);


		}
	}
}
