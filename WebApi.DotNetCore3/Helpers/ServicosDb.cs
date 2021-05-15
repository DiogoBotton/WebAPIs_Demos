using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DotNetCore3.Contexts;
using WebApi.DotNetCore3.Domains;

namespace WebApi.DotNetCore3.Helpers
{
    public static class ServicosDb
    {
        public static void AddDefaultValuesDb(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                // Retorna o serviço de contexto do banco de dados, pois o método AddDefualtValues requisita o mesmo
                AddDefaultValues(serviceScope.ServiceProvider.GetService<ProdutosContext>());
            }
        }

        public static void AddDefaultValues(ProdutosContext ctx)
        {
            if (!ctx.Produtos.Any())
            {
                ctx.Produtos.AddRange(new List<Produto>()
                {
                    new Produto("Redmi 7", "Xiaomi", 800, 10),
                    new Produto("Redmi 8", "Xiaomi", 900, 15),
                });

                ctx.SaveChanges();
            }
        }
    }
}
