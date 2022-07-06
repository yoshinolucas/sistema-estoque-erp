using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sistema_ERP.Interfaces;
using Sistema_ERP.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_ERP.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IUnitOfWork _inutOfWork;
        public ProdutoController(IUnitOfWork inutOfWork)
        {
            _inutOfWork = inutOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _inutOfWork.Produtos.GetAllAsync();
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> CriarProduto()
        {
            var data = await _inutOfWork.Categorias.GetAllAsync();
            ViewData["opcoesCategoria"] = new SelectList(data.ToList(), "Id_Categoria", "Nome");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CriarProduto(Produto produto)
        {
            await _inutOfWork.Produtos.AddAsync(produto);
            var data = await _inutOfWork.Categorias.GetAllAsync();
            ViewData["opcoesCategoria"] = new SelectList(data.ToList(), "Id_Categoria", "Nome", produto.Id_Categoria);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditarProduto(int id)
        {
            var data = await _inutOfWork.Produtos.GetByIdAsync(id);
            var dataCategoria = await _inutOfWork.Categorias.GetAllAsync();
            ViewData["opcoesCategoria"] = new SelectList(dataCategoria.ToList(), "Id_Categoria", "Nome", data.Id_Categoria);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> EditarProduto(Produto produto)
        {
            await _inutOfWork.Produtos.UpdateAsync(produto);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> ExcluirProduto(int id)
        {
            Produto produto = await _inutOfWork.Produtos.GetByIdAsync(id);
            return View(produto);
        }

        public async Task<IActionResult> ExcluirProdutoDefinitivo(int id)
        {
            await _inutOfWork.Produtos.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
