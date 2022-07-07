using Microsoft.AspNetCore.Mvc;
using Sistema_ERP.Helper;
using Sistema_ERP.Interfaces;
using Sistema_ERP.Models;
using System;
using System.Threading.Tasks;

namespace Sistema_ERP.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISessao _sessao;
        public LoginController(IUnitOfWork unitOfWork, ISessao sessao)
        {
            _unitOfWork = unitOfWork;
            _sessao = sessao;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // Se o usuario ja estiver logado
            if(_sessao.BuscarSessaoDoUsuario() != null) return RedirectToAction("Index", "Home");

            return View();
        }

        public IActionResult Sair()
        {
            _sessao.RemoverSessaoDoUsuario();
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public async Task<IActionResult> Entrar(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = await _unitOfWork.
                    Usuarios
                    .GetByLoginAsync(loginModel.Login);
                if(usuario != null)
                {
                    if (usuario.SenhaValida(loginModel.Senha))
                    {
                        _sessao.CriarSessaoDoUsuario(usuario);
                        return RedirectToAction("Index", "Home");
                    }

                    TempData["MensagemErro"] = $"Senha do usuário inválido. tente novamente.";
                    
                }
                TempData["MensagemErro"] = $"Usuário e/ou senha inválido(s). Por favor, tente novamente.";
            }           
            return View("Index");
        }
    }
}
