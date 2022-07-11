using Microsoft.AspNetCore.Mvc;
using Sistema_ERP.Filters;
using Sistema_ERP.Interfaces;
using Sistema_ERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_ERP.Controllers
{
    [PagUsuarioLogado]
    public class CategoriaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private const int PAGE_SIZE = 10;
        public CategoriaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Index(
            int? ordem = 0,
            int pg = 1,
            string searchText = ""
            )
        {
            IReadOnlyList<Categoria> categorias;
           
            if (searchText != "" && searchText != null)
            {
                categorias = await _unitOfWork
                            .Categorias
                            .GetByNomeSearchAsync(searchText);                   
            }
            else
            {
                categorias = await _unitOfWork
                            .Categorias
                            .GetAllAsync();
            }

            if (ordem.HasValue)
            {
                switch (ordem.Value)
                {
                    case 1: categorias = await _unitOfWork
                            .Categorias.GetByNomeAsync(); break;
                    case 2: categorias = await _unitOfWork
                            .Categorias.GetByNomeDescAsync(); break;
                }
            }
            else
            {
                categorias = await _unitOfWork
                            .Categorias
                            .GetAllAsync();
            }
            if (pg < 1)
                pg = 1;

            int recsCount = categorias.Count();
            var pager = new Pager(recsCount, pg, PAGE_SIZE);
            int recSkip = (pg - 1) * PAGE_SIZE;
            var data = categorias.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;

            
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
            if (ModelState.IsValid)
            {
                await _unitOfWork.Categorias.AddAsync(categoria);
                TempData["CategoriaCriada"] = $"Categoria {categoria.Nome} criada com sucesso!";
                return RedirectToAction("Index");
            }
            return View(categoria);
            
        }

        [HttpGet]
        public async Task<IActionResult> EditarCategoria(int id)
        {
            Categoria categoria = await _unitOfWork.Categorias.GetByIdAsync(id);
            return View(categoria);
        }

        [HttpPost]
        public async Task<IActionResult> EditarCategoria(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.Categorias.UpdateAsync(categoria);
                TempData["CategoriaEditada"] = $"Categoria {categoria.Nome} editada com sucesso!";
                return RedirectToAction("Index");
            }
            return View(categoria);
            
        }

        [HttpGet]
        public async Task<IActionResult> ExcluirCategoria(int id)
        {
            Categoria categoria = await _unitOfWork.Categorias.GetByIdAsync(id);
            return View(categoria);
        }

        public async Task<IActionResult> ExcluirCategoriaDefinitivo(int id)
        {
            await _unitOfWork.Categorias.DeleteAsync(id);
            TempData["CategoriaExcluida"] = $"Categoria excluída com sucesso!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DetalhesCategoria(int id)
        {
            Categoria categoria = await _unitOfWork.Categorias.GetByIdAsync(id);
            return View(categoria);
        }
    }
}
