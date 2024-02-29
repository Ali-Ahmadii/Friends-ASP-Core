using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyContaxt _RealDb;
        List<Friend> _Friends;

        public HomeController(ILogger<HomeController> logger,MyContaxt RealDb)
        {
            _logger = logger;
            _RealDb = RealDb;
        }

        public async Task<IActionResult> Index()
        {
            int flag_for_upperfcase = 0;
            if(flag_for_upperfcase == 0)
            {
                UpperCaser u = new UpperCaser(_RealDb);
                _Friends = _RealDb.Friends.ToList();
                for (int i = 0; i < _Friends.Count(); i++)
                {
                    BackgroundJob.Schedule(() => u.Uppercaser(_Friends[i]), TimeSpan.FromSeconds(5));
                }
                flag_for_upperfcase = 1;
            }
            return View();
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
