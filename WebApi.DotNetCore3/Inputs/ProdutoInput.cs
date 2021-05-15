using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DotNetCore3.Inputs
{
    public class ProdutoInput
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Fabricante { get; set; }
        [Required]
        public double ValorUnidade { get; set; }
        [Required]
        public int QtdEstoque { get; set; }
    }
}
