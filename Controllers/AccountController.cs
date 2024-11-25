using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RunGroupTUT.Data;
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

        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel registerDTO)
		{
			if (!ModelState.IsValid)
			{
				return View(registerDTO);
			}
			
			var user = await userManager.FindByEmailAsync(registerDTO.EmailAddress);
			if (user != null)
			{
				TempData["Error"] = "This email is already in use";
				return View(registerDTO);
			}
			var newUser = new AppUser
			{
				Email = registerDTO.EmailAddress,
				UserName = registerDTO.EmailAddress,
			};
			var newUserResponse = await userManager.CreateAsync(newUser,registerDTO.Password);
			if (newUserResponse.Succeeded)
			{
				await userManager.AddToRoleAsync(newUser, UserRoles.Admin);
			}
			else
			{
				TempData["Error"] = newUserResponse.Errors.ElementAt(0).Description;
				return View(registerDTO);
			}

            return RedirectToAction("Index", "Race");
        }

        [HttpPost]
		public async Task<IActionResult> Logout()
		{
			await signInManager.SignOutAsync();
			return RedirectToAction("Index","Race");
		}
    }
}
