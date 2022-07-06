﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _unitOfWork
                .Categorias
                .GetAllAsync();
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
    }
}
