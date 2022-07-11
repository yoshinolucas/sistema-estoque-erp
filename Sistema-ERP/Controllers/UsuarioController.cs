using Microsoft.AspNetCore.Mvc;
using Sistema_ERP.Filters;
using Sistema_ERP.Interfaces;
using Sistema_ERP.Models;
using System.Threading.Tasks;

namespace Sistema_ERP.Controllers
{
    [PagRestritaOnlyAdmin]
    public class UsuarioController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private const int PAGE_SIZE = 5;
        public UsuarioController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _unitOfWork.Usuarios.GetAllAsync();
            return View(data);
        }

        [HttpGet]
        public IActionResult CriarUsuario()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CriarUsuario(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.Usuarios.AddAsync(usuario);
                TempData["UsuarioCriado"] = $"Usuário {usuario.Nome} criado com sucesso";
                return RedirectToAction("Index");
            }
            return View(usuario);
            
        }

        [HttpGet]
        public async Task<IActionResult> EditarUsuario(int id)
        {
            Usuario usuario = await _unitOfWork.Usuarios.GetByIdAsync(id);
            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> EditarUsuario(Usuario usuario)
        {
            if(ModelState.IsValid)
            {
                await _unitOfWork.Usuarios.UpdateAsync(usuario);
                TempData["UsuarioEditado"] = $"Usuário {usuario.Nome} editado com sucesso";
                return RedirectToAction("Index");
            }
            return View(usuario);
            
        }

        [HttpGet]
        public async Task<IActionResult> ExcluirUsuario(int id)
        {
            Usuario usuario = await _unitOfWork.Usuarios.GetByIdAsync(id);
            return View(usuario);
        }

        public async Task<IActionResult> ExcluirUsuarioDefinitivo(int id)
        {
            await _unitOfWork.Usuarios.DeleteAsync(id);
            TempData["UsuarioExcluido"] = "Usuário excluído com sucesso";
            return RedirectToAction("Index");
        }

    }
}
