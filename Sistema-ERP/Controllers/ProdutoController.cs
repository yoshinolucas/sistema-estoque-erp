using Microsoft.AspNetCore.Mvc;
using Sistema_ERP.Interfaces;

namespace Sistema_ERP.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoRepository _repo;
        public ProdutoController(IProdutoRepository repo)
        {
            _repo = repo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CriarProduto()
        {
            return View();
        }

        public IActionResult EditarProduto()
        {
            return View();
        }

        public IActionResult ExcluirProduto()
        {
            return View();
        }
    }
}
