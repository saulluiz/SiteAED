using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TrabalhoAed.Models;

namespace TrabalhoAed.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            Console.WriteLine("oi");

            return View();
        }
        [HttpPost]
        public IActionResult Create([FromForm] PeopleModel pessoa )
        {
            Console.WriteLine(pessoa.DateOfBirth);
            return View("Index");
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





