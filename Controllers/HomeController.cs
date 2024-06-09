using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Security.Cryptography.Xml;
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
        public IActionResult Read()
        {
            Lista lista = new Lista();
            lista.Add(new PeopleModel() { UserId="1",FirstName="Leandro",LastName="Barbara",Sex="apaixonado",Email="leandrin@gmail",Phone="teleforne",JobTitle="apaixonado"});;lista.Add(new PeopleModel() { UserId="1",FirstName="Saulo",LastName="Barbara",Sex="apaixonado",Email="leandrin@gmail",Phone="teleforne",JobTitle="apaixonado"});;

            
            return View(lista);
        } 
        
        
        [HttpGet]
        public IActionResult Creating(int id)
        {
            
            return View(SexModel.sexTypes());
        }
        
        [HttpPost]
        public IActionResult Creating(int id,[FromForm] PeopleModel pessoa )
        {
            FileModifier.Create( PeopleFiles.GetFile(id), pessoa );

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





