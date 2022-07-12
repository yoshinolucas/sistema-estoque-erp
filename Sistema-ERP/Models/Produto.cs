using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Sistema_ERP.Models
{
    public class Produto
    {
        public int Id_Produto { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]        
        public decimal? Preco { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [Range(0, double.MaxValue, ErrorMessage = "Insira um valor válido")]
        public int? Estoque { get; set; }
        [Required(ErrorMessage = "Escolha uma opção válida")]
        public int? Id_Categoria { get; set; }
        public Categoria Categoria { get; set; }
        public DateTime Data_Criada { get; set; }
        public DateTime? Data_Modificada { get; set; }

    }
}
