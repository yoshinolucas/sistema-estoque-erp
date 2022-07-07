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
        private readonly IEmail _email;
        public LoginController(IUnitOfWork unitOfWork, ISessao sessao, IEmail email)
        {
            _unitOfWork = unitOfWork;
            _sessao = sessao;
            _email = email;
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

        [HttpPost]
        public async Task<IActionResult> EnviarLinkParaRedefinirSenha(RedefinirSenhaModel rsm)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = await _unitOfWork.
                    Usuarios
                    .GetByEmailLoginAsync(rsm.Email, rsm.Login);
                if (usuario != null)
                {
                    string novaSenha = usuario.GerarNovaSenha();
                    
                    string mensagem = $"Sua nova senha é: {novaSenha}";
                    bool emailEnviado = _email.Enviar(usuario.Email, "Sistema ERP - Nova Senha", mensagem);

                    if (emailEnviado)
                    {
                        await _unitOfWork.Usuarios.UpdateSenhaAsync(usuario);
                        TempData["MensagemSucesso"] = $"Enviamos uma nova senha para o seu e-mail.";
                    }
                    else
                    {
                        TempData["MensagemErro"] = $"Não conseguimos enviar o e-mail, favor tentar novamente";
                    }        
                    RedirectToAction("Index", "Login");
                }
                TempData["MensagemErro"] = $"Usuário e/ou e-mail inválido(s). Por favor, verifique seus dados.";
            }
            return View("Index");
        }

        public IActionResult RedefinirSenha()
        {
            return View();
        }
    }
}
