using Microsoft.AspNetCore.Mvc;

namespace EverisStore.API.Controllers
{
    public class ProdutosController : BaseController
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}