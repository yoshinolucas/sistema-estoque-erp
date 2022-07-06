using System;
using System.Collections.Generic;

namespace Sistema_ERP.Models
{
    public class Categoria
    {
        public int Id_Categoria { get; set; }
        public string Nome { get; set; }
        public List<Produto> Produtos { get; set; }
        public DateTime Data_Criada { get; set; }
        public DateTime? Data_Modificada { get; set; }
    }
}
