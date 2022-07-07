using Microsoft.AspNetCore.Mvc;
using Sistema_ERP.Filters;

namespace Sistema_ERP.Controllers
{
    [PagUsuarioLogado]
    public class RestritoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
