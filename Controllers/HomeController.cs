using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RunGroupTUT.Helpers;
using RunGroupTUT.Interfaces;
using RunGroupTUT.ViewModels;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
        private readonly IClubRepository clubRepository;

        public HomeController(ILogger<HomeController> logger,IClubRepository clubRepository)
		{
			_logger = logger;
            this.clubRepository = clubRepository;
        }

		public async Task<IActionResult> Index()
		{
			var ipinfo = new IPInfo();
			var homeDTO = new HomeViewModel();
			try
			{
				string url = "https://ipinfo.io?token=815be49516ea39";
				var info = new WebClient().DownloadString(url);
				ipinfo = JsonConvert.DeserializeObject<IPInfo>(info);
				RegionInfo myRegionInfo = new RegionInfo(ipinfo.Country);
				ipinfo.Country = myRegionInfo.EnglishName;
				homeDTO.City = ipinfo.City;
				homeDTO.State = ipinfo.Region;
				if (homeDTO.City != null)
				{
					homeDTO.Clubs = await clubRepository.GetClubByCity(homeDTO.City);
				}
				else
				{
					homeDTO.Clubs = null;
                }
				return View(homeDTO);
			}
			catch {
				homeDTO.Clubs = null;
			}
			return View(homeDTO);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}