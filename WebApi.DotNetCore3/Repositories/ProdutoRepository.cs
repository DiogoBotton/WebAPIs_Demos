using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DotNetCore3.Contexts;
using WebApi.DotNetCore3.Domains;
using WebApi.DotNetCore3.Interfaces;

namespace WebApi.DotNetCore3.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        public ProdutosContext _ctx { get; set; }

        // Acredito que só é possível construtor de repositório com o próprio contexto como paramêtro, apenas se houver injeção de dependência
        public ProdutoRepository(ProdutosContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Produto> Create(Produto produto)
        {
            // Adiciona produto assíncronamente
            var produtoCreated = _ctx.Produtos.AddAsync(produto);

            // Salva as alterações assíncronamente
            await _ctx.SaveChangesAsync();

            // Retorna o resultado da tarefa
            // return produtoCreated.Result.Entity;
            return await Task.FromResult(produtoCreated.Result.Entity);
        }

        public List<Produto> GetAllProdutos()
        {
            return _ctx.Produtos.ToList();
        }

        public Produto GetProdutoById(int id)
        {
            return _ctx.Produtos.FirstOrDefault(x => x.Id == id);
        }
    }
}
