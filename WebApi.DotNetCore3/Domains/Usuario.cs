using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DotNetCore3.Domains
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public Usuario(string email, string senha)
        {
            Email = email;
            Senha = senha;
        }
    }
}
