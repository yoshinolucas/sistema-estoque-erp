using System.ComponentModel.DataAnnotations;

namespace Sistema_ERP.Models
{
    public class RedefinirSenhaModel
    {
        [Required(ErrorMessage = "digite seu login")]
        public string Login { get; set; }
        [Required(ErrorMessage = "digite seu email")]
        public string Email { get; set; }
    }
}
