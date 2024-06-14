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
        public IActionResult Read(int id)
        {
            FileModifier.ReadFile(PeopleFiles.GetFile(id));

            return View(new { page = id, LIST = DB.LIST });
        }
        [HttpGet]
        public IActionResult Delete(string id)
        {
            var arr = id.Split("?");
            string page = arr[0];
            string peopleToDelete = arr[1];
            Console.WriteLine(peopleToDelete);
            //delete a pessoa de id == peopleTODelete
            return RedirectToAction("Read", new { id = page });
        }
       
        [HttpGet]
        public IActionResult UpdatePage(string id)
        {
            var arr = id.Split("?");
            string page = arr[0];
            string peopleToUpdate = arr[1];
            Console.WriteLine("Update " + peopleToUpdate);
            return View(new { page = page,people=peopleToUpdate }); ;
        }


        [HttpPost]
        public IActionResult UpdatePage(string id, [FromForm] PeopleModel pessoa)
        {
            var arr = id.Split("?");
            string page = arr[0];
            string peopleToUpdate = arr[1];
            Console.WriteLine("Editando pessoa " + pessoa.FirstName + " " + peopleToUpdate);
            //Passa os dados do PeopleModel pessoa para a funcao de update

            return RedirectToAction("Read", new { id = page });
        }
        [HttpGet]
        public IActionResult SelectRead()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Creating(int id)
        {

            return View(SexModel.sexTypes());
        }

        [HttpPost]
        public IActionResult Creating(int id, [FromForm] PeopleModel pessoa)
        {
            FileModifier.Create(PeopleFiles.GetFile(id), pessoa);

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





