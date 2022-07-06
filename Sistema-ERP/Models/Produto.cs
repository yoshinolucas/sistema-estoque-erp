﻿using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Sistema_ERP.Models
{
    public class Produto
    {
        public int Id_Produto { get; set; }
        [Required(ErrorMessage = "Campo nome obrigatório")]
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Estoque { get; set; }
        public int Id_Categoria { get; set; }
        public Categoria Categoria { get; set; }
        public DateTime Data_Criada { get; set; }
        public DateTime? Data_Modificada { get; set; }

    }
}
