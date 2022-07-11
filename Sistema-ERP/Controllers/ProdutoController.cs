using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sistema_ERP.Filters;
using Sistema_ERP.Interfaces;
using Sistema_ERP.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_ERP.Controllers
{
    [PagUsuarioLogado]
    public class ProdutoController : Controller
    {
        private readonly IUnitOfWork _inutOfWork;
        private const int PAGE_SIZE = 5;
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
            if (ModelState.IsValid)
            {
                await _inutOfWork.Produtos.AddAsync(produto);
                var data = await _inutOfWork.Categorias.GetAllAsync();
                ViewData["opcoesCategoria"] = new SelectList(data.ToList(), "Id_Categoria", "Nome", produto.Id_Categoria);
                TempData["ProdutoCriado"] = $"Produto {produto.Nome} criado com sucesso!";
                return RedirectToAction("Index");
            }
            return View(produto);
            
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
            if (ModelState.IsValid)
            {
                await _inutOfWork.Produtos.UpdateAsync(produto);
                TempData["ProdutoEditado"] = $"Produto {produto.Nome} editado com sucesso!";
                return RedirectToAction("Index");
            }
            return View(produto);
            
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
            TempData["ProdutoExcluido"] = $"Produto excluído com sucesso!";
            return RedirectToAction("Index");
        }
    }
}
