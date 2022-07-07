using Sistema_ERP.Enums;
using Sistema_ERP.Helper;
using System;
using System.ComponentModel.DataAnnotations;

namespace Sistema_ERP.Models
{
    public class Usuario
    {
        public int Id_Usuario { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Senha { get; set; }
        public PerfilEnum Perfil { get; set; }
        public DateTime Data_Criada { get; set; }
        public DateTime? Data_Modificada { get; set; }

        public bool SenhaValida(string senha)
        {
            return Senha == senha.GerarHash();
        }

        public void SetSenhaHash()
        {
            Senha = Senha.GerarHash();
        }
    }
}
