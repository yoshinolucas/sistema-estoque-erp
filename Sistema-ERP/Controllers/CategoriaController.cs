using Microsoft.AspNetCore.Mvc;
using Sistema_ERP.Interfaces;
using Sistema_ERP.Models;
using System;
using System.Threading.Tasks;

namespace Sistema_ERP.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _unitOfWork.Categorias.GetAllAsync();
            return View(data);
        }

        [HttpGet]
        public IActionResult CriarCategoria()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CriarCategoria(Categoria categoria)
        {
            await _unitOfWork.Categorias.AddAsync(categoria);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditarCategoria()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ExcluirCategoria()
        {
            return View();
        }
    }
}
