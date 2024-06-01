using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
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

            return View();
        } 
        
        
        [HttpGet]
        public IActionResult Creating(int id)
        {

            return View();
        }
        
       
        [HttpPost]
        public IActionResult Creating(int id,[FromForm] PeopleModel pessoa )
        {
            Console.WriteLine(id);
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





