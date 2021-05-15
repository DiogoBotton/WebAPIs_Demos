using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DotNetCore3.Domains
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Fabricante { get; set; }
        public double ValorUnidade { get; set; }
        public int QtdEstoque { get; set; }
        public double ValorEstoque => ValorUnidade * QtdEstoque;

        public Produto(string nome, string fabricante, double valorUnidade, int qtdEstoque)
        {
            Nome = nome;
            Fabricante = fabricante;
            ValorUnidade = valorUnidade;
            QtdEstoque = qtdEstoque;
        }
    }
}
