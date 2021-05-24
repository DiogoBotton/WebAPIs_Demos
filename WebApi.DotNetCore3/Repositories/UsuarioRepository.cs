using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DotNetCore3.Contexts;
using WebApi.DotNetCore3.Domains;
using WebApi.DotNetCore3.Interfaces;

namespace WebApi.DotNetCore3.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public ProdutosContext _ctx { get; set; }

        public UsuarioRepository(ProdutosContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Usuario> Create(Usuario usuario)
        {
            // Adiciona produto assíncronamente
            var usuarioCreated = _ctx.Usuarios.AddAsync(usuario);

            // Salva as alterações assíncronamente
            await _ctx.SaveChangesAsync();

            // Retorna o resultado da tarefa
            // return produtoCreated.Result.Entity;
            return await Task.FromResult(usuarioCreated.Result.Entity);
        }

        public List<Usuario> GetAllUsuarios()
        {
            return _ctx.Usuarios.ToList();
        }

        public Usuario GetUsuarioById(int id)
        {
            return _ctx.Usuarios.FirstOrDefault(x => x.Id == id);
        }

        public Usuario GetUsuarioByEmailSenha(string email, string senha)
        {
            return _ctx.Usuarios.FirstOrDefault(x => x.Email == email && x.Senha == senha);
        }
    }
}
