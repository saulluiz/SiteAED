using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using site_aed.Models;
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
        public IActionResult Read(int id)
        {
            FileModifier.ReadFile(PeopleFiles.GetFile(id));
            return View(new { page = id, LIST = DB.LIST });
        }
        [HttpPost]
        public IActionResult Read(int id, [FromForm] SearchModel query)
        {

            Lista l1=new Lista();
            //enviar uma lista a pagina utilizando o metodo de pesquisa otimizado (Serach ainda � sequencial)
            //sugestao: Criar metodo que receba os parametros do tipo da pesquisa e do valor da pesquisa e ,a partir disso
            //retorne uma lista [Essa lista deve ser passada para View abaixo, com os parametros dentro do objeto

            switch(query.valueType)
            {
                case "email":
                    Console.WriteLine("Email");
                    l1.Add(DB.LIST.Search((p) => p.Email, query.value));
                    break;
                case "UserId":
                default:
                    Console.WriteLine("user ID");
                    l1.Add(DB.LIST.Search((p) => p.UserId, query.value));
                    break;
            }

            return View(new { page = id, LIST = l1 });
        }
        [HttpGet]
        public IActionResult Delete(string id)
        {
            var arr = id.Split("?");
            string page = arr[0];
            string peopleToDelete = arr[arr.Length - 1];
            DB.Delete(peopleToDelete, int.Parse(page));
            return RedirectToAction("Read", new { id = page });
        }
        [HttpGet]
        public IActionResult UpdatePage(string id)
        {
            var arr = id.Split("?");
            string page = arr[0];
            string peopleToUpdate = arr[arr.Length - 1];

            return View(new { page = page, people = peopleToUpdate }); ;
        }

        [HttpPost]
        public IActionResult UpdatePage(string id, [FromForm] PeopleModel pessoa)
        {
            var arr = id.Split("?");
            string page = arr[0];
            string peopleToUpdate = arr[arr.Length - 1];
            DB.Update(pessoa, int.Parse(page));
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
            return View();
        }
        [HttpGet]
        public IActionResult Ordenar(string id)
        {
            var arr = id.Split("?");
            string page = arr[0];
            string parametroOrdenacao = arr[arr.Length - 1];
            if (DB.CurrentFile != PeopleFiles.GetFile(int.Parse(page)))
                FileModifier.ReadFile(PeopleFiles.GetFile(int.Parse(page)));
            switch (parametroOrdenacao)
            {
                case "FirstName":
                    DB.LIST.Sort((p) => p.FirstName);
                    break;
                case "LastName":
                    DB.LIST.Sort((p) => p.LastName);
                    break;
            }
            return View("Read", new { page = id, LIST = DB.LIST });
        }
        [HttpPost]
        public IActionResult Creating(int id, [FromForm] PeopleModel pessoa)
        {
            DB.Create(pessoa, id);
            return View("Index");
        }
   
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}





