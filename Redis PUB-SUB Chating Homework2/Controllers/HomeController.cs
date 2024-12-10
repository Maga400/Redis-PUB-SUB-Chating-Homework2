using Microsoft.AspNetCore.Mvc;
using Redis_PUB_SUB_Chating_Homework2.Models;
using System.Diagnostics;

namespace Redis_PUB_SUB_Chating_Homework2.Controllers
{
    public class HomeController : Controller
    {
        


        //var muxer = ConnectionMultiplexer.Connect(
        //    new ConfigurationOptions
        //    {
        //        EndPoints = { { "redis-13898.c261.us-east-1-4.ec2.redns.redis-cloud.com", 13898 } },
        //        User = "default",
        //        Password = "Bjc65AaWcTrZ9JC1e3OLamCFZP6FeCY4"
        //    }
        //);

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SubmitName(string name)
        {
            // Name değerini alır ve işlem yapar
            ViewBag.Message = $"Received name: {name}";
            return View("Index"); // Gerekirse başka bir View'e yönlendirin
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
