using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DotNetCore3.Domains;

namespace WebApi.DotNetCore3.Interfaces
{
    public interface IProdutoRepository
    {
        Task<Produto> Create(Produto produto);
        List<Produto> GetAllProdutos();
        Produto GetProdutoById(int id);
    }
}
